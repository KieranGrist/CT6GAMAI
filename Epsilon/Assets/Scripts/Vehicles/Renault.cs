using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
/// <summary>
/// Class to handle the Renault vehicles
/// </summary>
public class Renault : Vehicle
{

 
    // Start is called before the first frame update
    void Start()
    {
        mass = Random.Range(650, 800);  //Minium of 650 maxium of 800
        DefaultMass = mass; //Set default mass to be mass     
        maxSpeed = Random.Range(15, 25);     //Max Speed kept between 15 - 25;     
        acceleration = maxSpeed / 0.4f;    //Mas speed divided by 0.4
        GetComponent<Renderer>().material.color = Color.yellow; //Set colour to be yellow
    }
}
