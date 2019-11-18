using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vehicle))]
[System.Serializable]
public class SteeringBehaviours : MonoBehaviour
{

    Vehicle vehicle; //Reference to the vehicicle attached to the current object
    [Header("Seek")]
    //SeekOn - Variables to control Seeks Behavior
    public bool IsSeekOn = false;  //If on AI will seek to a choosen location
    public Vector2 SeekOnTargetPos; // Location to seek to 
    public float SeekOnStopDistance; //Distance from the target, the AI will stop at the distancve
    [Header("Wander")]
    //WanderOn
    public bool IsWanderOn = false; //If on the AI will randomly move around the map
    public float WanderRadius = 10f; // Sets how large the wander radius is, if its outside this radius it will move backinside
    public float TurnChance = 0.05f; //How often the AI can turn, the higher this is the more it will jitter
    public Vector2 WanderTarget = Vector2.zero; // Sets the target to 0 
    public Vector2 wanderForce; //Vector that stores the current wander force 

    [Header("Obstacle")]
    //Obstacle Avodience On
    public bool IsObstacleAvoidenceOn = false; //If on the AI will attempt to avoid obstables in its radius
    public GameObject ObstacleClosetGameObject; //The closet object to the player
    public Vector2 ObstacleForce; // Vector that stores the current force applied to get away from that object
    public float BoxSize = 2; // Size of the collision box
    [Header("Evade")]
    //Evade On
    public bool IsEvadeOn = false;
    [Header("Pursue")]
    //Pursue On
    public bool IsPursueOn = false;
    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }
    ///<summary>Calculates the current vehicle force </summary>
    public Vector2 Calculate()
    {
        //Sets the velocity sum to 0
        Vector2 VelocitySum = Vector2.zero;
        //Calculation ifs, if their bool is true they will calculate their force and add it to the velocity sum
        if (IsSeekOn)
        {
            if (Vector2.Distance(transform.position, SeekOnTargetPos) <= SeekOnStopDistance)
            {
                //We're close enough to "stop"
                IsSeekOn = false;

                //Set the vehicle's velocity back to zero
                vehicle.Velocity = Vector2.zero;
            }
            else
                VelocitySum += Seek(SeekOnTargetPos);
        }
        if (IsWanderOn)
            VelocitySum += Wander();
        if (IsObstacleAvoidenceOn)
            VelocitySum += ObstacleAvoidence();
        return VelocitySum;
    }
    ///<summary>Goes towards from a  location </summary>
    Vector2 Seek(Vector2 TargetPos)
    {

        Vector2 DesiredVelocity = ((TargetPos - new Vector2(transform.position.x, transform.position.z)).normalized) * vehicle.MaxSpeed;

        return (DesiredVelocity - vehicle.Velocity);
    }
    ///<summary>Goes away from a  location </summary>
    Vector2 Flee(Vector2 TargetPos)
    {
        Vector2 DesiredVelocity = ((new Vector2(transform.position.x, transform.position.z) - TargetPos).normalized) * vehicle.MaxSpeed;

        return (DesiredVelocity - vehicle.Velocity);
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
    void Pursuit(Vehicle Evader)
    {
        Vector2 ToEvader = Evader.transform.position - transform.position;
        float RelativeHeading = Vector2.Dot(transform.forward.normalized, Evader.transform.forward.normalized);
        if (RelativeHeading > 0)
            Seek(Evader.transform.position);
        var LookAheadTime = ToEvader.magnitude / (vehicle.MaxSpeed + Evader.Velocity.magnitude);
        var EvaderFuturePosition = new Vector2(Evader.transform.position.x, Evader.transform.position.z) + Evader.Velocity * LookAheadTime;
        Seek(EvaderFuturePosition);
    }
    ///<summary>Runs away from a Pursuer </summary>
    void Evade(Vehicle Pursuer)
    {
        Vector2 ToPursuer = Pursuer.transform.position - transform.position;
        var LookAheadTime = ToPursuer.magnitude / (vehicle.MaxSpeed + Pursuer.Velocity.magnitude);
        var PursuerFuturePosition =new Vector2(transform.position.x, transform.position.z) + Pursuer.Velocity * LookAheadTime;
        Flee(PursuerFuturePosition);
    }
    ///<summary>Returns a force to Wander towards </summary>
    Vector2 Wander()
    {
        //https://gamedev.stackexchange.com/questions/106737/wander-steering-behaviour-in-3d
        if (transform.position.magnitude > WanderRadius)
        {
            var directionToCenter = (WanderTarget - new Vector2(transform.position.x,transform.position.z)).normalized;
            wanderForce = vehicle.Velocity.normalized + directionToCenter;
        }
        else if (Random.value < TurnChance)
            wanderForce = GetRandomWanderForce();
        wanderForce = new Vector2(wanderForce.x, wanderForce.y); //Ensures the force does not increase/decrease the height of ARTIE
        return wanderForce;
    }
    ///<summary>Returns a random Wander circle </summary>
    Vector2 GetRandomWanderForce()
    {
        //https://gamedev.stackexchange.com/questions/106737/wander-steering-behaviour-in-3d
        var randomPoint = Random.insideUnitCircle;
        var displacement = new Vector2(randomPoint.x, randomPoint.y);
        displacement = Quaternion.LookRotation(vehicle.Velocity) * displacement;
        Vector2 wanderForce = vehicle.Velocity.normalized + displacement;
        return wanderForce.normalized;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(vehicle.transform.position , new Vector3(BoxSize , BoxSize, BoxSize ));
    }
    ///<summary>Will Avoid the Obstacles by going around the obstacle  </summary>
    Vector2 ObstacleAvoidence()
    {
        float Distance = float.MaxValue;
        Collider ClosetObject = new Collider();
        Vector2 LocalPosOfClosestObstacle = new Vector2();
        foreach (var item in Physics.OverlapBox(transform.position, new Vector3(BoxSize, BoxSize, BoxSize)))
            if (Vector2.Distance(transform.position, item.transform.position) < Distance && item.CompareTag("Obstacles"))
            {
                Vector2 LocalPos = transform.InverseTransformPoint(item.transform.position);

                if (LocalPos.x >= 0)
                {
                    var ExpandedRadius = item.bounds.size.magnitude + vehicle.GetComponent<Collider>().bounds.size.magnitude;
                    if (Mathf.Abs(LocalPos.y) < ExpandedRadius)
                    {
                        var cX = LocalPos.x;
                        var cY = LocalPos.y;
                        var SqrtPart = Mathf.Sqrt(ExpandedRadius * ExpandedRadius - cY * cY);

                        var ip = cX - SqrtPart;

                        if (ip <= 0.0)
                            ip = cX + SqrtPart;
                        if (ip < Distance)
                        {
                            Distance = ip;

                            ClosetObject = item;
                            ObstacleClosetGameObject = item.gameObject;
                            LocalPosOfClosestObstacle = LocalPos;
                        }
                    }
                }
            }
        Vector2 SteeringForce = new Vector2();
        if (ClosetObject)
        {
            //the closer the agent is to an object, the stronger the 
            //steering force should be
            var multiplier = 1.0f + (BoxSize - LocalPosOfClosestObstacle.x) /
                                BoxSize;

            //calculate the lateral force
            SteeringForce.y = (ClosetObject.bounds.size.magnitude -
                               LocalPosOfClosestObstacle.y) * multiplier;

            //apply a braking force proportional to the obstacles distance from
            //the vehicle. 
           var BrakingWeight = 0.2f;

            SteeringForce.x = (ClosetObject.bounds.size.magnitude -
                               LocalPosOfClosestObstacle.x) *
                               BrakingWeight;
        }
        ObstacleForce = transform.TransformPoint(SteeringForce);
        ObstacleForce = new Vector2(ObstacleForce.x, ObstacleForce.y) * 5 ;
        return ObstacleForce;
    }
    ///<summary>Will Avoid the walls by going in the Opposite direction of the wall </summary>
    void WallAvoidence()
    {

    }

    /// <summary>
    /// Will Seek to TargetPos until within StopDistance range from it
    /// </summary>
    /// <param name="TargetPos"></param>
    /// <param name="StopDistance"></param>
    public void SeekOn(Vector2 TargetPos, float StopDistance = 0.01f)
    {
        IsSeekOn = true;
        SeekOnTargetPos = TargetPos;
        SeekOnStopDistance = StopDistance;
    }
    ///<summary>Turns On Seek </summary>
    public void SeekOff()
    {
        IsSeekOn = false;
    }
    ///<summary>Turns On Wander </summary>
    public void WanderOn()
    {
        IsWanderOn = true;
    }
    ///<summary>Turns Off Wander </summary>
    public void WanderOff()
    {
        IsWanderOn = false;
        vehicle.Velocity = Vector2.zero;
    }
    ///<summary>Turns On Obstacle Avoidence</summary>
    public void ObstacleAvodienceOn()
    {
        IsObstacleAvoidenceOn = true;
    }
    ///<summary>Turns Off Obstacle Avoidence</summary>
    public void ObstacleAvodienceOff()
    {
        IsObstacleAvoidenceOn = false;
    }
}
