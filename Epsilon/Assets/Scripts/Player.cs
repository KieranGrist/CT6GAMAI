using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Vector3 Velocity;
    float Speed;
    public float MaxSpeed = 5;
    public float SteeringForce = 2;
    public static Player player;
    Rigidbody rb;
    // Start is called before the first frame update
    void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        player = this;  
    }
    public void PlacePlayer(int GridSlot)
    {
        transform.position = Grids.Grid.GridList[GridSlot].Value.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 Force = transform.forward;
        Speed = MaxSpeed;
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, -SteeringForce, 0));
        if (Input.GetKey(KeyCode.W))
            Force += transform.forward * Speed;
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(new Vector3(0, SteeringForce, 0));
        if (Input.GetKey(KeyCode.S))
            Force -= transform.forward * Speed * Time.deltaTime;
            //transform.position += Velocity * Time.deltaTime;
            if (rb.velocity.magnitude > MaxSpeed)
        {
            Force = -transform.forward;
        }
        rb.AddForce(Force);
    }
}
