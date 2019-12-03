using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;
    
public class SteeringBehaviours : MonoBehaviour
{
    public bool NOAI = false;
    [Header("Total Velocity")]
    public Vector2 Velocity;
    Vehicle _vehicle; //Reference to the vehicle attached to the current object
    [Header("Seek")]
    //SeekOn - Variables to control Seeks Behavior
    public bool isSeekOn = false;  //If on AI will seek to a chosen location
    public Vector2 seekOnTargetPos; // Location to seek to 
    public float seekOnStopDistance; //Distance from the target, the AI will stop at the distance
    [Header("Wander")]
    //WanderOn
    public bool isWanderOn = false; //If on the AI will randomly move around the map
    public float wanderRadius = 10f; // Sets how large the wander radius is, if its outside this radius it will move back inside that radius
    public float turnChance = 0.05f; //How often the AI can turn, the higher this is the more it will jitter
    private readonly Vector2 _wanderTarget = Vector2.zero; // Sets the target to 0 
    public Vector2 wanderForce; //Vector that stores the current wander force 

    [Header("Obstacle")]
    //Obstacle Avodience On
    public bool isObstacleAvoidanceOn = false; //If on the AI will attempt to avoid obstacles in its radius
    private GameObject obstacleClosetGameObject; //The closet object to the player
    public Vector2 obstacleForce; // Vector that stores the current force applied to get away from that object
    public ProjCube ProjectedCube;
    private bool foundObject = false;
    public float BoxSize =2;
    [Header("Wall")]
    //Obstacle Wall avodience On
    public bool isWallAvodienceOn = false;
    public Vector2 wallForce; // Vector that stores the current force applied to get away from that object
    public float WallAvodidenceDistance = 2;


    [Header("Overtake")]
    public bool isOvertakeOn;
    public Vector2 overtakeForce; // Vector that stores the current force applied to get away from that object
    private void Start()
    {
        _vehicle = GetComponent<Vehicle>();
    }
    private void Update()
    {
        if (NOAI)
        {
            ProjectedCube = GetComponentInChildren<ProjCube>();
            _vehicle.Accelerate(Calculate());
            Vector3 norm = _vehicle.Velocity.normalized;
            transform.forward = new Vector3(norm.x, 0, norm.y);
        }
        
    }
    ///<summary>Calculates the current vehicle force </summary>
    public Vector2 Calculate()
    {
        //Sets the velocity sum to 0
        Vector2 velocitySum = Vector2.zero;
        //Calculation ifs, if their bool is true they will calculate their force and add it to the velocity sum
  
        if (isObstacleAvoidanceOn)
            velocitySum += ObstacleAvoidance();
        if (isWallAvodienceOn)
            velocitySum += WallAvoidence();
        if (isOvertakeOn)
            velocitySum += Overtake ();
        if (isSeekOn)
        {
            if (Vector3.Distance(transform.position, new Vector3(seekOnTargetPos.x, 10, seekOnTargetPos.y)) <= seekOnStopDistance)
            {
                //We're close enough to "stop"
                isSeekOn = false;

                //Set the vehicle's velocity back to zero
                //TODO: Talk to jay to make sure I can do without setting it back to 0 due to it being vehicles
                //   _vehicle.Velocity = Vector2.zero; 
            }
            else
                velocitySum += Seek(seekOnTargetPos);
        }
        Velocity = velocitySum;
        return velocitySum;
    }
    ///<summary>Goes towards from a  location </summary>
    private Vector2 Seek(Vector2 targetPos)
    {

        Vector2 desiredVelocity = ((targetPos - new Vector2(transform.position.x, transform.position.z)).normalized) * _vehicle.MaxSpeed;

        return (desiredVelocity - _vehicle.Velocity);
    }
    ///<summary>Goes away from a  location </summary>
    private Vector2 Flee(Vector2 targetPos)
    {
        Vector2 desiredVelocity = ((new Vector2(transform.position.x, transform.position.z) - targetPos).normalized) * _vehicle.MaxSpeed;

        return (desiredVelocity - _vehicle.Velocity);
    }
    ///<summary>Slows down when close to a location </summary>
    Vector2 Arrive(Vector2 TargetPos)
    {
        Vector2 ToTarget = TargetPos - new Vector2(transform.position.x, transform.position.z);
        float Distance = ToTarget.magnitude;
        float SlowingDistance = 0;
        if (Distance > 5)
        {
            SlowingDistance = 3;
        }
        float Speed = Distance / SlowingDistance;
        Vector2 DesiredVelocity = ToTarget.normalized * Speed / Distance;
        return DesiredVelocity;
    }
    ///<summary>Runs towards from a Evader </summary>
    ///<summary>Will Avoid the Obstacles by going around the obstacle  </summary>
    Vector2 ObstacleAvoidance()
    {
        ProjectedCube.transform.localScale = new Vector3(gameObject.GetComponent<Collider>().bounds.size.x, ProjectedCube.transform.localScale.y, BoxSize + (_vehicle.Velocity.magnitude / _vehicle.MaxSpeed) * BoxSize);
        //Make sure the box is transformed infront of the player
        ProjectedCube.transform.localPosition = new Vector3(0, 0, ProjectedCube.transform.localScale.z / 2);
        var boxSize = BoxSize +
                  (_vehicle.Speed / _vehicle.MaxSpeed) *
                  BoxSize;
        obstacleClosetGameObject = null;
   
        float distance = float.MaxValue;
        Collider closetObject = new Collider();
        foundObject = false;
        Vector2 localPosOfClosestObstacle = new Vector2();

        foreach (var item in Physics.OverlapBox(transform.position, ProjectedCube.transform.localScale, transform.rotation))
            if (Vector2.Distance(transform.position, item.transform.position) < distance &&
                (item.CompareTag("Dynamic Obstacles")) && item != gameObject)
            {
                Vector2 LocalPos = transform.InverseTransformPoint(item.transform.position);

     
                    var ExpandedRadius = item.GetComponent<Collider>().bounds.size.magnitude +
                                         _vehicle.GetComponent<Collider>().bounds.size.magnitude;
                    if (Mathf.Abs(LocalPos.y) < ExpandedRadius)
                    {
                        var cX = LocalPos.x;
                        var cY = LocalPos.y;
                        var SqrtPart = Mathf.Sqrt(ExpandedRadius * ExpandedRadius - cY * cY);

                        var ip = cX - SqrtPart;

                        if (ip <= 0.0)
                            ip = cX + SqrtPart;
                        if (ip < distance)
                        {
                            distance = ip;
                            foundObject = true;
                            closetObject = item.GetComponent<Collider>();
                            obstacleClosetGameObject = item.gameObject;
                            localPosOfClosestObstacle = LocalPos;
                        }
                    }
                
            }

        Vector2 steeringForce = new Vector2();
        if (foundObject)
        {
            //the closer the agent is to an object, the stronger the 
            //steering force should be
            var multiplier = 1.0f + (boxSize - localPosOfClosestObstacle.x) /
                                boxSize;
            var mag = closetObject.bounds.size.magnitude;
            //calculate the lateral force
            steeringForce.y = (mag -
                               localPosOfClosestObstacle.y) * multiplier;


            var BrakingWeight = 0.002f;

            //apply a braking force proportional to the obstacles distance from
            //the vehicle. 
            steeringForce.x = (mag -
                               localPosOfClosestObstacle.x) *
                               BrakingWeight;
              Vector3  TransformedForce = transform.TransformPoint(new Vector3(steeringForce.x, 0, steeringForce.y));
                obstacleForce = new Vector2(TransformedForce.z, TransformedForce.x);
            return  new Vector2(TransformedForce.z, TransformedForce.x);
        }

        return new Vector2();
    }
    ///<summary>Will Avoid the walls by going in the Opposite direction of the wall </summary>
    Vector2 WallAvoidence()
    {
        Vector3 Norm = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        var Feelers = new[]
        {
           transform.position + (Norm * WallAvodidenceDistance),
           transform.position + Quaternion.AngleAxis(45, Vector3.up) * Norm * (WallAvodidenceDistance / 2),
           transform.position + Quaternion.AngleAxis(-45, Vector3.up) * Norm * (WallAvodidenceDistance / 2)
        };
        var DistToThis = 0.0f;
        var DistToClosest = float.MaxValue;
        Collider ClosestWall = new Collider();
        Vector2 SteeringForce;  //holds the closest intersection point
        Vector2 ClosestPoint = new Vector2();
        SteeringForce = new Vector2();
        LayerMask mask = LayerMask.GetMask("Walls");
        foreach (var item in Feelers)
        {  
            if (Physics.Raycast(transform.position, item - transform.position, out RaycastHit hit, WallAvodidenceDistance, mask))
            {
                Debug.DrawRay(transform.position, item - transform.position, Color.red);
                DistToThis = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(item.x, item.z));
                if (DistToThis < DistToClosest)
                {
                    DistToClosest = DistToThis;
                    ClosestWall = hit.collider;
                    ClosestPoint = new Vector2(hit.normal.x, hit.normal.z);
                }
            }
            else
                Debug.DrawRay(transform.position, item - transform.position, Color.blue);

            if (ClosestWall && hit.collider)
            {
                Vector2 OverShoot = new Vector2(hit.transform.position.x, hit.transform.position.z) - ClosestPoint;
                SteeringForce = new Vector2(hit.normal.x, hit.normal.z)* OverShoot.magnitude;
            }
        }

        wallForce = SteeringForce;
        return SteeringForce;
    }

    /// <summary>
    /// Obstacle Avoidance but for AI
    /// </summary>
    /// <returns></returns>
    Vector2 Overtake ()
    {
        ProjectedCube.transform.localScale = new Vector3(gameObject.GetComponent<Collider>().bounds.size.x, ProjectedCube.transform.localScale.y, BoxSize + (_vehicle.Velocity.magnitude / _vehicle.MaxSpeed) * BoxSize);
        //Make sure the box is transformed infront of the player
        ProjectedCube.transform.localPosition = new Vector3(0, 0, ProjectedCube.transform.localScale.z / 2);
        var boxSize = BoxSize +
                  (_vehicle.Speed / _vehicle.MaxSpeed) *
                  BoxSize;
        obstacleClosetGameObject = null;

        float distance = float.MaxValue;
        Collider closetObject = new Collider();
        foundObject = false;
        Vector2 localPosOfClosestObstacle = new Vector2();

        foreach (var item in Physics.OverlapBox(transform.position, ProjectedCube.transform.localScale, transform.rotation))
            if (Vector2.Distance(transform.position, item.transform.position) < distance &&
                (item.CompareTag("AI") || item.CompareTag("Agent")) && item.gameObject != gameObject)
            {
                Vector2 LocalPos = transform.InverseTransformPoint(item.transform.position);


                var ExpandedRadius = item.GetComponent<Collider>().bounds.size.magnitude +
                                     _vehicle.GetComponent<Collider>().bounds.size.magnitude;
                if (Mathf.Abs(LocalPos.y) < ExpandedRadius)
                {
                    var cX = LocalPos.x;
                    var cY = LocalPos.y;
                    var SqrtPart = Mathf.Sqrt(ExpandedRadius * ExpandedRadius - cY * cY);

                    var ip = cX - SqrtPart;

                    if (ip <= 0.0)
                        ip = cX + SqrtPart;
                    if (ip < distance)
                    {
                        distance = ip;
                        foundObject = true;
                        closetObject = item.GetComponent<Collider>();
                        obstacleClosetGameObject = item.gameObject;
                        localPosOfClosestObstacle = LocalPos;
                    }
                }

            }

        Vector2 steeringForce = new Vector2();
        if (foundObject)
        {
            //the closer the agent is to an object, the stronger the 
            //steering force should be
            var multiplier = 1.0f + (boxSize - localPosOfClosestObstacle.x) /
                                boxSize;
            var mag = closetObject.bounds.size.magnitude;
            //calculate the lateral force
            steeringForce.y = (mag -
                               localPosOfClosestObstacle.y) * multiplier;


            var BrakingWeight = 0.002f;

            //apply a braking force proportional to the obstacles distance from
            //the vehicle. 
            steeringForce.x = (mag -
                               localPosOfClosestObstacle.x) *
                               BrakingWeight;
            Vector3 TransformedForce = transform.TransformPoint(new Vector3(steeringForce.x, 0, steeringForce.y));
            overtakeForce = TransformedForce;
            return TransformedForce;
        }

        return new Vector2();
    }
    /// <summary>
    /// Will Seek to TargetPos until within StopDistance range from it
    /// </summary>
    /// <param name="TargetPos"></param>
    /// <param name="StopDistance"></param>
    public void SeekOn(Vector2 TargetPos, float StopDistance = 0.01f)
    {
        isSeekOn = true;
        seekOnTargetPos = TargetPos;
        seekOnStopDistance = StopDistance;
    }
    ///<summary>Turns On Seek </summary>
    public void SeekOff()
    {
        isSeekOn = false;
    }
    ///<summary>Turns On Wander </summary>
    public void WanderOn()
    {
        isWanderOn = true;
    }
    ///<summary>Turns Off Wander </summary>
    public void WanderOff()
    {
        isWanderOn = false;
    }
    ///<summary>Turns On Obstacle Avoidence</summary>
    public void ObstacleAvodienceOn()
    {
        isObstacleAvoidanceOn = true;
    }
    ///<summary>Turns Off Obstacle Avoidence</summary>
    public void ObstacleAvodienceOff()
    {
        isObstacleAvoidanceOn = false;
    }
    public void WallAvodienceOn()
    {
        isWallAvodienceOn = true;
    }
    ///<summary>Turns Off Obstacle Avoidence</summary>
    public void WallAvodienceOff()
    {
        isWallAvodienceOn = false;
    }
    public void OvertakeOn()
    {
        isOvertakeOn = true;
    }
    public void OvertakeOff()
    {
        isOvertakeOn = false;
    }
}
