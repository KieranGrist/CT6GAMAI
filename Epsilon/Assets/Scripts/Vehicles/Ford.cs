using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ford : Vehicle
{
    void Start()
    {
        mass = 666;
        DefaultMass = mass;
        maxSpeed = 19;
        acceleration = 9;
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
