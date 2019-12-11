using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class which manages the pit lane
/// </summary>
public class Pitlane : MonoBehaviour
{
   
    public GameObject FordPit, FerrariPit, MercedesPit, RenaultPit;  //Game objects of the pits 
    public static Pitlane pitlane; //Static reference to the script
    // Start is called before the first frame update
    void Awake()
    {
        pitlane = this; //Set reference to be this 
        FordPit.GetComponent<Renderer>().material.color = Color.blue; //Set colour of object to be blue
        FerrariPit.GetComponent<Renderer>().material.color = Color.red; //Set colour of object to be red
        MercedesPit.GetComponent<Renderer>().material.color = Color.black; //Set colour of object to be black
        RenaultPit.GetComponent<Renderer>().material.color = Color.yellow; //Set colour of object to be yellow
    }
    private void OnTriggerEnter(Collider other)
    {
        var vic = other.GetComponent<Vehicle>(); //gets the vehicle component of the collider
        if (vic) //If vic exists       
            vic.PitEnter(); //Call pit enter script
    }
    private void OnTriggerExit(Collider other)
    {
        var vic = other.GetComponent<Vehicle>(); //gets the vehicle component of the collider
        if (vic) //If vic exists       
            vic.PitExit(); //Call pit enter script
    }
    // Update is called once per frame
    void Update()
    {
       
           pitlane = this; //Set reference to be this
    }
}
