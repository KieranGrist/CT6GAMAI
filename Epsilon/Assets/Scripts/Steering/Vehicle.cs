using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringBehaviours))]
public class Vehicle : MonoBehaviour {

    /////////////////////
    //Updated Values
    /////////////////////
    /// <summary>
    /// This is applied to the current position every frame
    /// </summary>
    public Vector2 Velocity;

    //Position, Heading and Side can be accessed from the transform component with transform.position, transform.forward and transform.right respectively

    //"Constant" values, they are public so we can adjust them through the editor

    //Represents the weight of an object, will effect its acceleration
    public float Mass = 1;

    //The maximum speed this agent can move per second
    public float MaxSpeed = 15;

    //The thrust this agent can produce
    public float MaxForce = 1;
    //We use this to determine how fast the agent can turn, but just ignore it for, we won't be using it
    public float MaxTurnRate = 1.0f;

    public Vector2 SteeringForce;
    public SteeringBehaviours SB;
    public Vector2 Heading;
    public Vector2 Side;
    // Use this for initialization
    void Start()
    {
        SB = GetComponent<SteeringBehaviours>();
    }
    private void OnDrawGizmos()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        SteeringForce = SB.Calculate();
        Vector2 Acceleration = SteeringForce / Mass;       
        Velocity += Acceleration; 

        Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);
        Heading = Velocity.normalized; 
        if (Velocity != Vector2.zero)
        {
            transform.position += new Vector3(Velocity.x, 0 , Velocity.y) * Time.deltaTime;
            Vector3 norm = Velocity.normalized;
            transform.forward = new Vector3(norm.x, 0, norm.y);
        }
 
        //transform.right should update on its own once we update the transform.forward
    }
}
