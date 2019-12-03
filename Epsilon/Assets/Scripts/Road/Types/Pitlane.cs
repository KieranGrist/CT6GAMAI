using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitlane : MonoBehaviour
{   
    public GameObject FordPit, FerrariPit, MercedesPit, RenaultPit;
    public static Pitlane pitlane;
    // Start is called before the first frame update
    void Awake()
    {
        pitlane = this;
        FordPit.GetComponent<Renderer>().material.color = Color.blue;
        FerrariPit.GetComponent<Renderer>().material.color = Color.red;
        MercedesPit.GetComponent<Renderer>().material.color = Color.black;
        RenaultPit.GetComponent<Renderer>().material.color = Color.yellow;
    }
    private void OnTriggerEnter(Collider other)
    {
        var vic = other.GetComponent<Vehicle>();
        if (vic)
        {
            vic.PitEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var vic = other.GetComponent<Vehicle>();
        if (vic)
        {
            vic.PitExit();
        }
    }
    // Update is called once per frame
    void Update()
    {
       
           pitlane = this;
    }
}
