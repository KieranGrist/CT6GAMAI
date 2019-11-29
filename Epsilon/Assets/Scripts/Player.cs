using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static Player player;
    Rigidbody rb;
    public Vehicle vehicle;
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
