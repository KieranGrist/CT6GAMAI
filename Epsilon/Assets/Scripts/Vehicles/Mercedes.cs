using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercedes : Vehicle 
{
    // Start is called before the first frame update
    void Start()
    {
        mass = 690;
        maxSpeed = 18;
        acceleration = 475;
        turningForce = 4;
        breakPower = 1.1F;
        GetComponent<Renderer>().material.color = new Color(192, 192, 192);
    }
}
