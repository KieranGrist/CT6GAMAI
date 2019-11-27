using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
   
    float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Speed = 5;
        if (Input.GetKey(KeyCode.LeftShift))
            Speed = 10;
            if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * Speed * Time.deltaTime;       
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(new Vector3(0, 1, 0));
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(new Vector3(0,-1, 0));
    }
}
