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
        maxSpeed = 18;
        acceleration = 420;
        turningForce = 4;
        breakPower = 1.1F;
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
