using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferrari : Vehicle
{
    // Start is called before the first frame update
    void Start()
    {
        mass = 672;
        maxSpeed = 17;
        acceleration = 511;
        turningForce = 4;
        breakPower = 1.1F;
        GetComponent<Renderer>().material.color = Color.red;
    }


}
