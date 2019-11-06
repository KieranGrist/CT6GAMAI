using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Highway : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Highway Tile. ID: " + Index;

      
        Cost = 2;
        Reset();
    }
    public override void Start()
    {
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;
        Cost = 2;
    }

    public override void Update()
    {
        name = "Highway Tile. ID: " + Index;
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;

    }
}
