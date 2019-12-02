using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ford : Vehicle
{
    void Start()
    {
        mass = 666;
        maxSpeed = 17;
        acceleration = 592;
        turningForce = 4;
        breakPower = 1.1F;
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
