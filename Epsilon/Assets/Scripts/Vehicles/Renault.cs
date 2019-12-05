using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Renault : Vehicle
{

 
    // Start is called before the first frame update
    void Start()
    {
        mass = 736;
        DefaultMass = mass;
        maxSpeed = 17;
        acceleration = 9;
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
