using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
[System.Serializable]

public class ReadOnlyAttribute : PropertyAttribute
{

}
//https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

public abstract class Vehicle :MonoBehaviour
{


    [Header("Lap")]
    [ReadOnly] public int CurrentLap;
    [ReadOnly] public int LastCheckPoint;
    [Header("Position in Race")]
    [ReadOnly] public int RacePosition;
    [Header("Time")]
    [ReadOnly] public TimeSpan LapTime;
    [ReadOnly] DateTime StartTime;
    [ReadOnly] public float CurrentLapTime =0;
    [ReadOnly] public float PreviusLapTime =0;
    [ReadOnly] public float Difference =0;



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
    protected bool BoostEnabled;
    protected bool PitSpeed;
    protected bool StopActive = false;
    protected float TriggerTimer = 2;
    public bool PerformingStop()
    {
        return StopActive;
    }
    public void PitEnter()
    {
        PitSpeed = true;
    }
    public void PitExit()
    {
        StopActive = false;
        PitSpeed = false;   
    }
    public void PerformStop()
    {
        StopActive = true;
        fuel += 1;
        fuel = Mathf.Clamp(fuel, -1, 100);
        if (fuel >= 98)
        {
            StopActive = false;
        }

    }
    public void BoostOn()
    {
        SpeedBoost = maxSpeed * 1.2f;
        BoostEnabled = true;
    }
    public void BoostOff()
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
        TriggerTimer += Time.deltaTime;
        if (transform.position.y < -10)
            Reset();
        transform.position += new Vector3(Velocity.x, 0, Velocity.y) * Time.deltaTime;
        velocity = Vector2.Lerp(velocity, velocity * Time.smoothDeltaTime, Time.smoothDeltaTime);
        speed = velocity.magnitude;
        CurrentLapTime = (float)LapTime.TotalSeconds;
        Difference = CurrentLapTime - PreviusLapTime;
    }
    public void Reset()
    {
        velocity = new Vector2();
        var ARTIE = GetComponent<AIAgent>();
               if (ARTIE)
        {
            transform.position = lastCheckpoint.transform.position;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoints"))
        {
            lastCheckpoint = other.gameObject;
            LastCheckPoint = other.gameObject.GetComponent<Checkpoint>().CheckpointTag;
        }
        if (TriggerTimer >= 2)
        {        
            if (other.CompareTag("Checkpoints"))
            {
                TriggerTimer = 0;
                var FinishLine = other.GetComponent<Checkpoint>().CheckpointTag;
                if (FinishLine == 1)
                {
                    CurrentLap++;
                    LapManager.manager.VehiclePassedCheckPoint(other.gameObject.GetComponent<Checkpoint>().CheckpointTag, this);
                }
            }

        }


    }
    public void StopRacing()
    {
        fuel = 0;
        velocity = new Vector2();
        GetComponent<AIAgent>().enabled = false;
        GetComponent<SteeringBehaviours>().enabled = false;
        GetComponent<Vehicle>().enabled = false;
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
                        Force *= acceleration;
                        velocity += new Vector2(Force.x, Force.y) / Mass;
                        velocity = Vector2.ClampMagnitude(velocity, SpeedBoost);
                 
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
