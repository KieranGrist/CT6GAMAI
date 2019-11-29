using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float Health;
    public static Player player;
    Rigidbody rb;
    public Vehicle vehicle;
    // Start is called before the first frame update
    void Awake ()
    {
        Health = 100;
           rb = GetComponent<Rigidbody>();
        player = this;  
    }

    private void OnCollisionEnter  (Collision collision)
    {
        if(collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("AI") || collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Dynamic Obstacles"))
        Health -= 10;
    }
    public void PlacePlayer(int GridSlot)
    {
        transform.position = Grids.Grid.GridList[GridSlot].Value.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.position.y < -1)
        {
            Health -= 10;
            rb.velocity = new Vector3();
        }
           
        if (Health <= 0)
        {
            Health = 100;
            transform.position = RaceTrack.raceTrack.FinishLine.transform.localPosition + new Vector3(0,2,0);
            vehicle.Reset();
        }

        rb.mass = vehicle.mass;
        if (Input.GetKey(KeyCode.A))
            vehicle.TurnLeft();
        if (Input.GetKey(KeyCode.W))
            vehicle.Accelerate();
        if (Input.GetKey(KeyCode.D))
            vehicle.TurnRight();
        if (Input.GetKey(KeyCode.S))
            vehicle.Brake();

    }
}
