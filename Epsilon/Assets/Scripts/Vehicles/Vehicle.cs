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



    protected float mass = 1;
    protected float acceleration = 2;
    protected float maxSpeed = 15;
    protected float DefaultMass;

    private float fuelUsedPerLap = 0;
    private float speed = 0;
    private float fuel = 100;
    private Vector2 velocity;

    private float NewMass;
    private float PreviousFuel = 0;
    private GameObject lastCheckpoint;
    private float SpeedBoost;
    private bool BoostEnabled;
    private bool PitSpeed;
    private bool StopActive = false;
    private float TriggerTimer = 2;

    public float Mass { get => mass; set => mass = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float FuelUsedPerLap { get => fuelUsedPerLap; set => fuelUsedPerLap = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Fuel { get => fuel; set => fuel = value; }
    public Vector2 Velocity { get => velocity; set => velocity = value; }

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
        PreviousFuel = fuel;
        StartTime = DateTime.Now;
    }
    public void EndLap()
    {

        fuelUsedPerLap =  PreviousFuel - fuel;
        PreviusLapTime = CurrentLapTime;
        LapTime = DateTime.Now - StartTime;
    }

    private void Update()
    {
        NewMass = DefaultMass + fuel;
        mass = NewMass; 
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
        GetComponent<AIAgent>().enabled = false;
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
            fuel -= Time.smoothDeltaTime *0.25f;   
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
}
