using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Residential : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Residential Tile. ID: " + Index;
        Cost = 6;
        Reset();
    }
    public override void Start()
    {
        GetComponent<Renderer>().material = MaterialManager.ResidentialMat;
        Cost = 6;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.ResidentialMat;
        name = "Residential Tile. ID: " + Index;
    }
    
}
