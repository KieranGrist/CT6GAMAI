using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercedes : Vehicle 
{
    // Start is called before the first frame update
    void Start()
    {
        mass = 690;
        DefaultMass = mass;
        maxSpeed = 18;
        acceleration = 9;
        GetComponent<Renderer>().material.color = Color.black;
    }
}
