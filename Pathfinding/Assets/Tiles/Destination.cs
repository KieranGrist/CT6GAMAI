using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Destination : TileNode
{
    public Color color;
    void Awake()
    {
        Distance = 1.25f;
        name = "Destination";
        color = Color.red;
    //    GetComponent<Renderer>().sharedMaterial.color = Color.red;
        Cost = 0;
        Reset();
    }
    public override void Start()
    {
        Cost = 0;
  
    }

    public override void Update()
    {
    //    GetComponent<Renderer>().sharedMaterial.color = Color.red;
    }
}   
