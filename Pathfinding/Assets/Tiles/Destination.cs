using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Destination : Tile
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Destination";
        GetComponent<Renderer>().sharedMaterial.color = Color.red;
        Cost = 0;
        Reset();
    }
    public override void Start()
    {
        Cost = 0;
  
    }

    public override void Update()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
}   
