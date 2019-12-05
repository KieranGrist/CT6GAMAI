using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferrari : Vehicle
{
    // Start is called before the first frame update
    void Start()
    {
        mass = 672;
        DefaultMass = mass;
        maxSpeed = 20;
        acceleration = 9;
        GetComponent<Renderer>().material.color = Color.red;
    }


}
