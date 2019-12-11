using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

 public class SteeringBehaviours : MonoBehaviour
{
    [Header("Total Velocity")]
 public    Vector2 Velocity;
    Vehicle _vehicle; //Reference to the vehicle attached to the current object
    [Header("Seek")]
    //SeekOn - Variables to control Seeks Behavior
     bool isSeekOn = false;  //If on AI will seek to a chosen location
    public Vector2 seekOnTargetPos; // Location to seek to 
     float seekOnStopDistance; //Distance from the target, the AI will stop at the distance

    [Header("Obstacle")]
    //Obstacle Avodience On
     bool isObstacleAvoidanceOn = false; //If on the AI will attempt to avoid obstacles in its radius
    public Vector2 obstacleForce; // Vector that stores the current force applied to get away from that object
    public ProjCube ProjectedCube; //Projected cube 
     float BoxSize = 2;  //Minium size of box

    [Header("Overtake")]
     bool isOvertakeOn; //If on the ai will attempt to overtake the vehicle infront of it
    public Vector2 overtakeForce; // Vector that stores the current force applied to get away from that object
    private void Start()
    {
        _vehicle = GetComponent<Vehicle>(); //Get the vehicle component of the game object 
    }
    ///<summary>Calculates the current vehicle force </summary>
    public Vector2 Calculate()
    {
        //Sets the velocity sum to 0
        Vector2 velocitySum = Vector2.zero;
        //Calculation ifs, if their bool is true they will calculate their force and add it to the velocity sum

        if (isObstacleAvoidanceOn) //if Obstacle Avoidance On
            velocitySum += ObstacleAvoidance(); //Increase velocity sum to be that of Obstacle Avoidances return value
        if (isOvertakeOn) //if Overtake is on 
            velocitySum += Overtake();  //Increase velocity sum to be that of Overtake Avoidances return value
        if (isSeekOn) //If seek is on 
        {
            if (Vector3.Distance(transform.position, new Vector3(seekOnTargetPos.x, transform.position.y, seekOnTargetPos.y)) <= seekOnStopDistance) //if  AI is close enough
            {
                //We're close enough to "stop"
                isSeekOn = false;   
            }
            else
                velocitySum += Seek(seekOnTargetPos); //Seek to the target position  and increase velocity sum by the return value 
        }
        Velocity = velocitySum; // Velocity is set to velocity sum  
        return velocitySum; //return velocity sum 
    }
    ///<summary>Goes towards from a  location </summary>
    private Vector2 Seek(Vector2 targetPos)
    {

        Vector2 desiredVelocity = ((targetPos - new Vector2(transform.position.x, transform.position.z)).normalized) * _vehicle.MaxSpeed; //Set desired velocity to be target position minus the normalised transform position and then multiply it by the vehicle max speed

        return (desiredVelocity - _vehicle.Velocity); //Subtract the velocity by the vehicle velocity and return that value
    }

    ///<summary>Will Avoid the Obstacles by going around the obstacle  </summary>
    Vector2 ObstacleAvoidance()
    {
        obstacleForce = new Vector2(); //Set obstacleForce to be a blank vector 2
        ProjectedCube.transform.localScale = new Vector3(gameObject.GetComponent<Collider>().bounds.size.x, ProjectedCube.transform.localScale.y, BoxSize + (_vehicle.Velocity.magnitude / _vehicle.MaxSpeed) * BoxSize); //Set the detection box length to be  proportional to the agent's velocity and ensure it is within the box size
        ProjectedCube.transform.localPosition = new Vector3(0, 0, ProjectedCube.transform.localScale.z / 2);       //Make sure the box is transformed infront of the player
        var boxSize = BoxSize +
                  (_vehicle.Speed / _vehicle.MaxSpeed) *
                  BoxSize; //Set the box size to be the minium box sized added by the speed divded by the max speed then multiplied by the box size

        float distance = float.MaxValue; //Set distance to be max value 
        Collider closetObject = new Collider(); //create a new closest object
        var foundObject = false; //Set found object to be false
        Vector2 localPosOfClosestObstacle = new Vector2(); //Create a new local position of the closet Obstacle

        foreach (var item in ProjectedCube.CollidedObjects) //Loop through all the collided objects of the cube 
            if (Vector2.Distance(transform.position, item.transform.position) < distance &&
                (item.CompareTag("Dynamic Obstacles")) && item != gameObject) //if the distance is between the item and vehicles is less then the current distance and the item is a dynamic obstacle and its not the current object
            { 
                Vector2 LocalPos = transform.InverseTransformPoint(item.transform.position); //Transform position from world space to local and store it 


                var ExpandedRadius = item.GetComponent<Collider>().bounds.size.magnitude +
                                     _vehicle.GetComponent<Collider>().bounds.size.magnitude; //Add the two collider sizes toghther 
                if (Mathf.Abs(LocalPos.y) < ExpandedRadius) //if the absoloutle of local pos y is less then the expanded radius 
                {
                    var cX = LocalPos.x; //set cx to be local pos x 
                    var cY = LocalPos.y; //set cy to be the local pos y
                    var SqrtPart = Mathf.Sqrt(ExpandedRadius * ExpandedRadius - cY * cY); //Square root the expanded radius squared subtracted by the cy squared 

                    var ip = cX - SqrtPart; //set ip to be cx - square root part 

                    if (ip <= 0.0) //if ip less then or equal to 0 
                        ip = cX + SqrtPart; //ip is set to cx + square root part
                    if (ip < distance) //if ip is less then the distance 
                    {
                        distance = ip; //distance is set to ip 
                        foundObject = true; //Found object is equal to true 
                        closetObject = item.GetComponent<Collider>(); //closest object is set to the collider of item 
                        localPosOfClosestObstacle = LocalPos; //Set the local pos of closest obs to the local position value 
                    }
                }

            }

        Vector2 steeringForce = new Vector2(); //Create a new steering force
        if (foundObject) // If we have found on object
        {
            //the closer the agent is to an object, the stronger the 
            //steering force should be
            var multiplier = 1.0f + (boxSize - localPosOfClosestObstacle.x) / boxSize;
            var mag = closetObject.bounds.size.magnitude;
            //calculate the lateral force
            steeringForce.y = (mag -
                               localPosOfClosestObstacle.y) * multiplier;


            var BrakingWeight = 0.02f; //set the breaking weight 

            //apply a braking force proportional to the obstacles distance from
            //the vehicle. 
            steeringForce.x = (mag -
                               localPosOfClosestObstacle.x) *
                               BrakingWeight;

            Vector3 TransformedForce = transform.TransformPoint(new Vector3(steeringForce.x, 0, steeringForce.y)); //Transform the steering force to world space 
            obstacleForce = new Vector2(TransformedForce.z, TransformedForce.x); //Set the obstacle force to be the transformed force 
            return new Vector2(TransformedForce.z, TransformedForce.x); //return the new transformed force
        }

        return new Vector2(); //return a blank vector 2
    }



    /// <summary>
    /// Obstacle Avoidance but for AI
    /// </summary>
    /// <returns></returns>
    Vector2 Overtake()
    {
        obstacleForce = new Vector2(); //Set obstacleForce to be a blank vector 2
        ProjectedCube.transform.localScale = new Vector3(gameObject.GetComponent<Collider>().bounds.size.x, ProjectedCube.transform.localScale.y, BoxSize + (_vehicle.Velocity.magnitude / _vehicle.MaxSpeed) * BoxSize); //Set the detection box length to be  proportional to the agent's velocity and ensure it is within the box size
        ProjectedCube.transform.localPosition = new Vector3(0, 0, ProjectedCube.transform.localScale.z / 2);       //Make sure the box is transformed infront of the player
        var boxSize = BoxSize +
                  (_vehicle.Speed / _vehicle.MaxSpeed) *
                  BoxSize; //Set the box size to be the minium box sized added by the speed divded by the max speed then multiplied by the box size
        Collider closetObject = new Collider(); //create a new closest object
        Vector2 localPosOfClosestObstacle = new Vector2(); //Create a new local position of the closet Obstacle
        var item = ProjectedCube.CheckForAI(_vehicle);
        if (item) //if item exists
        {
            Vector2 LocalPos = transform.InverseTransformPoint(item.transform.position); //Transform position from world space to local and store it 


            var ExpandedRadius = item.GetComponent<Collider>().bounds.size.magnitude +
                                 _vehicle.GetComponent<Collider>().bounds.size.magnitude; //Add the two collider sizes toghther 
            if (Mathf.Abs(LocalPos.y) < ExpandedRadius) //if the absoloutle of local pos y is less then the expanded radius 
            {
                var cX = LocalPos.x; //set cx to be local pos x 
                var cY = LocalPos.y; //set cy to be the local pos y
                var SqrtPart = Mathf.Sqrt(ExpandedRadius * ExpandedRadius - cY * cY); //Square root the expanded radius squared subtracted by the cy squared 
                closetObject = item.GetComponent<Collider>(); //closest object is set to the collider of item 
                localPosOfClosestObstacle = LocalPos; //Set the local pos of closest obs to the local position value 
            }
            Vector2 steeringForce = new Vector2(); //Create a new steering force
                                                   //the closer the agent is to an object, the stronger the 
                                                   //steering force should be
            var multiplier = 1.0f + (boxSize - localPosOfClosestObstacle.x) / boxSize;
            var mag = closetObject.bounds.size.magnitude;
            //calculate the lateral force
            steeringForce.y = (mag -
                               localPosOfClosestObstacle.y) * multiplier;


            var BrakingWeight = 0.02f; //set the breaking weight 

            //apply a braking force proportional to the obstacles distance from
            //the vehicle. 
            steeringForce.x = (mag -
                               localPosOfClosestObstacle.x) *
                               BrakingWeight;

            Vector3 TransformedForce = transform.TransformPoint(new Vector3(steeringForce.x, 0, steeringForce.y)); //Transform the steering force to world space 
            obstacleForce = new Vector2(TransformedForce.z, TransformedForce.x); //Set the obstacle force to be the transformed force 
            return new Vector2(TransformedForce.z, TransformedForce.x); //return the new transformed force    
        }
        else
            return new Vector2(); //return a blank vector 2
    }
    /// <summary>
    /// Will Seek to TargetPos until within StopDistance range from it
    /// </summary>
    /// <param name="TargetPos"></param>
    /// <param name="StopDistance"></param>
    public void SeekOn(Vector2 TargetPos, float StopDistance = 0.01f)
    {
        isSeekOn = true; //set seek to true
        seekOnTargetPos = TargetPos; //set target position 
        seekOnStopDistance = StopDistance; //set stop distance
    }
    ///<summary>Turns On Seek </summary>
     void SeekOff()
    {
        isSeekOn = false; // set seek to false
    }
    ///<summary>Turns On Obstacle Avoidence</summary>
    public void ObstacleAvodienceOn()
    { 
        isObstacleAvoidanceOn = true; //set obstacle avoidence to be true
    }
    ///<summary>Turns Off Obstacle Avoidence</summary>
    public void ObstacleAvodienceOff()
    {
        isObstacleAvoidanceOn = false; //set obstacle avoidence to be false
    }
    /// <summary>
    /// Turns on overtaking
    /// </summary>
    public void OvertakeOn()
    {
        isOvertakeOn = true; //set overtake to be true
    }
    /// <summary>
    /// Turns off overtaking
    /// </summary>
    public void OvertakeOff()
    {
        isOvertakeOn = false; //set overtake to be false
    }
}
