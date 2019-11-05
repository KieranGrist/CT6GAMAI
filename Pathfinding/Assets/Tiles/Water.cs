using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Water : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Water";
        GetComponent<Renderer>().material = MaterialManager.WaterMat;
        Cost = 10;
        Reset();
    }

    public override void Start()
    {
        Cost = 10;
        Reset();
    }

    public override void Update()
    {      
        GetComponent<Renderer>().material = MaterialManager.WaterMat;
    }
}
