using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Renault : Vehicle
{

 
    // Start is called before the first frame update
    void Start()
    {
        mass = 712;
        maxSpeed = 20;
        acceleration = 500;
        turningForce = 4;
        breakPower = 1.1f;
    }
}
