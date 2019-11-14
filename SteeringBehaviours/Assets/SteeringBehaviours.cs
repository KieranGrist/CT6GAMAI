using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class SteeringBehaviours : MonoBehaviour {

    Vehicle vehicle;

    //SeekOn
    bool IsSeekOn = false;
    Vector3 SeekOnTargetPos;
    float SeekOnStopDistance;

    //WanderOn
    bool IsWanderOn = false;
    public float WanderRadius = 10f;
    public float WanderDistance = 10f;
    public float WanderJitter = 1f;
    Vector3 WanderTarget = Vector3.zero;

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
        Vector3 DesiredVelocity = (TargetPos - transform.position).normalized * vehicle.MaxSpeed;

        return (DesiredVelocity - vehicle.Velocity);
    }

    Vector3 Flee(Vector3 TargetPos)
    {
        //TO-DO: Implement this method

        return Vector3.zero;
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

        Vector3 targetWorld = transform.position + WanderTarget;

        targetWorld += transform.forward * WanderDistance;

        return targetWorld - transform.position;
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
