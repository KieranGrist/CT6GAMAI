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
        GetComponent<Renderer>().material = MaterialManager.ResidentialMat;
        Cost = 6;
        Reset();
    }
    public override void Start()
    {
        Cost = 6;
    }

    public override void Update()
    {
        name = "Residential Tile. ID: " + Index;
        GetComponent<Renderer>().material = MaterialManager.ResidentialMat;
    }
    
}
