using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class SteeringBehaviours : MonoBehaviour {

    public Vehicle vehicle;

    //SeekOn
    public bool IsSeekOn = false;
    public Vector3 SeekOnTargetPos;
    public float SeekOnStopDistance;

    //WanderOn
    public bool IsWanderOn = false;
    public float WanderRadius = 10f;
    public float WanderDistance = 10f;
    public float WanderJitter = 1f;
    public Vector3 WanderTarget = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        vehicle = GetComponent<Vehicle>();

        //Set an initial wander target
        WanderTarget = new Vector3(Random.Range(-WanderRadius, WanderRadius), 0, Random.Range(-WanderRadius, WanderRadius));
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public Vector3 Calculate()
    {
        Vector3 VelocitySum = Vector3.zero;

        if (IsSeekOn)
        {
            if (Vector3.Distance(transform.position, SeekOnTargetPos) <= SeekOnStopDistance)
            {
                //We're close enough to "stop"
                IsSeekOn = false;

                //Set the vehicle's velocity back to zero
                vehicle.Velocity = Vector3.zero;
            }
            else
            {
                VelocitySum += Seek(SeekOnTargetPos);
            }
        }

        if (IsWanderOn)
        {
            VelocitySum += Wander();
        }

        return VelocitySum;
    }

    Vector3 Seek(Vector3 TargetPos)
    {
        Vector3 DesiredVelocity = ((TargetPos - transform.position).normalized) * vehicle.MaxSpeed;

        return (DesiredVelocity - vehicle.Velocity);
    }

    Vector3 Flee(Vector3 TargetPos)
    {
        Vector3 DesiredVelocity = (( transform.position - TargetPos).normalized) * vehicle.MaxSpeed;

        return (DesiredVelocity - vehicle.Velocity);
    }
    Vector3 Arrive (Vector3 TargetPos)
    {     
        Vector3 ToTarget = TargetPos - transform.position;
        float Distance = ToTarget.magnitude;
        float SlowingDistance = 0;
        if (Distance > 5)
        {
            SlowingDistance = 3;
        }
        float Speed = Distance / SlowingDistance; 
        Vector3 DesiredVelocity = ToTarget.normalized * Speed / Distance;
        return DesiredVelocity;
    }
    void Pursuit(Vehicle Evader)
    {
        Vector3 ToEvader = Evader.transform.position - transform.position;
        float RelativeHeading =  Vector3.Dot(transform.forward.normalized, Evader.transform.forward.normalized);
        if (RelativeHeading > 0)
            Seek(Evader.transform.position);
        var LookAheadTime = ToEvader.magnitude / (vehicle.MaxSpeed + Evader.Velocity.magnitude);
        var EvaderFuturePosition = Evader.transform.position + Evader.Velocity * LookAheadTime;
        Seek(EvaderFuturePosition);
    }
    void Evade(Vehicle Pursuer)
    {
        Vector3 ToPursuer = Pursuer.transform.position - transform.position;
        var LookAheadTime = ToPursuer.magnitude / (vehicle.MaxSpeed + Pursuer.Velocity.magnitude);
        var PursuerFuturePosition = transform.position + Pursuer.Velocity * LookAheadTime;
        Flee(PursuerFuturePosition);
    }
    Vector3 Wander()
    {
        WanderTarget += new Vector3(
            Random.Range(-1f, 1f) * WanderJitter,
            0,
            Random.Range(-1f, 1f) * WanderJitter);

        WanderTarget.Normalize();

        WanderTarget *= WanderRadius;

        Vector3 targetLocal = WanderTarget;

        Vector3 TargetWorldPostion = vehicle.transform.position + WanderTarget;

        TargetWorldPostion += vehicle.transform.forward * WanderDistance;

        return TargetWorldPostion - transform.position;
    }
   void ObstacleAvoidence ()
    {

    }
    /// <summary>
    /// Will Seek to TargetPos until within StopDistance range from it
    /// </summary>
    /// <param name="TargetPos"></param>
    /// <param name="StopDistance"></param>
    public void SeekOn(Vector3 TargetPos, float StopDistance = 0.01f)
    {
        IsSeekOn = true;
        SeekOnTargetPos = TargetPos;
        SeekOnStopDistance = StopDistance;
    }

    public void WanderOn()
    {
        IsWanderOn = true;
    }

    public void WanderOff()
    {
        IsWanderOn = false;
        vehicle.Velocity = Vector3.zero;
    }
}
