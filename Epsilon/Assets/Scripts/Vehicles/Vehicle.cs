using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Vehicle :MonoBehaviour
{    /// <summary>
     /// Current Velocity of the vehicle
     /// </summary>
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

    public Vector2 velocity;

    public float mass = 1;

    public float acceleration = 2;

    public float speed = 0;

    public float turningForce;

    public float maxSpeed = 15;

    public float breakPower = 15;

    private void Update()
    {
        if (Velocity != Vector2.zero)
            transform.position += new Vector3(Velocity.x, 0, Velocity.y) * Time.deltaTime;
        velocity = Vector3.Lerp(velocity, velocity * Time.smoothDeltaTime, Time.smoothDeltaTime);
        speed = velocity.magnitude;
    }

    public void Accelerate()
    {

        Vector3 norm = Velocity.normalized;
        Vector3 Force = new Vector3(norm.x, 0, norm.y);

        if (Speed  <= MaxSpeed)
            Force = transform.forward * acceleration;   




 
        velocity += new Vector2(Force.x, Force.z) / Mass;
        velocity = Vector2.ClampMagnitude(velocity, MaxSpeed);
  

   

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
