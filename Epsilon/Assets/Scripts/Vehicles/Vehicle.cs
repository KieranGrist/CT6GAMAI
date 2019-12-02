using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public abstract class Vehicle :MonoBehaviour
{  
    [Header("Lap")]
    public int CurrentLap;
    public int LastCheckPoint;
    [Header("Position in Race")]
    public int Position;
    [Header("Time")]
    public TimeSpan LapTime;
    DateTime StartTime;
    public float CurrentLapTime =0;
    public float PreviusLapTime =0;
    public float Difference =0;
    /// <summary>
    /// Current Velocity of the vehicle
    /// </summary>
    /// 
    public Vector2 Velocity { get => velocity; }
    /// <summary>
    /// How Heavy the vehicle is
    /// </summary>
    public float Mass { get => mass; }

    /// <summary>
    /// How Quickly the vehicle can accelerate to top speed
    /// </summary>
    public float Acceleration { get => acceleration;  }
    /// <summary>
    /// CurrentSpeed Of Vehicle
    /// </summary>
    public float Speed { get => speed; }
    /// <summary>
    /// How Quickly the vehicle can turn 
    /// </summary>
    public float TurningForce { get => turningForce; }
    /// <summary>
    /// Max Speed of the vehicle
    /// </summary>
    public float MaxSpeed { get => maxSpeed; }
    /// <summary>
    /// Break Power of the vehicle
    /// </summary>
    public float BreakPower { get => breakPower; }
    public float Fuel { get => fuel; }

    protected Vector2 velocity;

    protected float mass = 1;

    protected float acceleration = 2;

    protected float speed = 0;

    protected float turningForce;

    protected float maxSpeed = 15;

    protected float breakPower = 15;
    protected float fuel = 100;
    protected GameObject lastCheckpoint;
    protected float SpeedBoost;
    bool BoostEnabled;
    bool PitSpeed;
    public void PitLane()
    {
        PitSpeed = true;
    }
    public void PerformStop()
    {
        fuel += 2 * Time.deltaTime;

    }
    public void BoostCar()
    {
        SpeedBoost = maxSpeed * 1.2f;
        BoostEnabled = true;
    }

    public void StartLap()
    {
         StartTime = DateTime.Now;
    }
    public void EndLap()
    {
        PreviusLapTime = CurrentLapTime;
        LapTime = DateTime.Now - StartTime;
    }

    private void Update()
    {
        if (transform.position.y < -10)
            Reset();
        transform.position += new Vector3(Velocity.x, 0, Velocity.y) * Time.deltaTime;
        velocity = Vector3.Lerp(velocity, velocity * Time.smoothDeltaTime, Time.smoothDeltaTime);
        speed = velocity.magnitude;
        CurrentLapTime = (float)LapTime.TotalSeconds;
        Difference = CurrentLapTime - PreviusLapTime;
    }
    public void Reset()
    {
        velocity = new Vector2();
        var ARTIE = GetComponent<AIAgent>();
        //       if (ARTIE)

    }   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoints"))
        {
            lastCheckpoint = other.gameObject;
            CurrentLap=   LapManager.manager.VehiclePassedCheckPoint(other.gameObject.GetComponent<Checkpoint>().CheckpointTag, this);
            LastCheckPoint = other.gameObject.GetComponent<Checkpoint>().CheckpointTag;
        }
  
    }

    public void Accelerate(Vector2 Force)
    {
        if (fuel > 0)
        {
            Vector3 norm = Velocity.normalized;
            if (!PitSpeed)
            {
                if (BoostEnabled)
                {
                    if (Speed <= SpeedBoost)
                    {
                        velocity += new Vector2(Force.x, Force.y) / Mass;
                        velocity = Vector2.ClampMagnitude(velocity, SpeedBoost);
                        Force *= acceleration;
                    }
                }
                else
                {
                    if (Speed <= MaxSpeed)
                        Force *= acceleration;
                    velocity += new Vector2(Force.x, Force.y) / Mass;
                    velocity = Vector2.ClampMagnitude(velocity, MaxSpeed);
                }
            }
            else
            {
                if (Speed <= 5)
                    Force *= acceleration;
                velocity += new Vector2(Force.x, Force.y) / Mass;
                velocity = Vector2.ClampMagnitude(velocity, 5);
            }
            fuel -= Time.deltaTime;   
        }

    }

    public void Accelerate()
    {
        if (fuel > 0)
        {
            Vector3 norm = Velocity.normalized;
            Vector3 Force = new Vector3(norm.x, 0, norm.y);

            if (Speed <= MaxSpeed)
                Force = transform.forward * acceleration;

            velocity += new Vector2(Force.x, Force.z) / Mass;
            velocity = Vector2.ClampMagnitude(velocity, MaxSpeed);

        }
   

    }
    public void TurnLeft()
    {
        transform.Rotate(new Vector3(0, -TurningForce, 0));
    }
    public void TurnRight()
    {
        transform.Rotate(new Vector3(0, TurningForce, 0));
    }
   public void Brake()
    {
        velocity /= BreakPower;
    }



}
