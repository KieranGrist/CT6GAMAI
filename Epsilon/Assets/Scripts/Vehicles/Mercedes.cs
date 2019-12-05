using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercedes : Vehicle 
{
    // Start is called before the first frame update
    void Start()
    {
        //Minium of 650 maxium of 800
        mass = Random.Range(650, 800);
        DefaultMass = mass;
        //Max Speed kept between 15 - 25;
        maxSpeed = Random.Range(15, 25);
        //Mas speed divided by 0.4
        acceleration = maxSpeed / 0.4f;
        GetComponent<Renderer>().material.color = Color.black;
    }
}
