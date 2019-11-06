using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Bridge : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Bridge Tile. ID: " + Index;
     
        Cost = 2;
        Reset();
    }
    public override void Start()
    {
        GetComponent<Renderer>().material = MaterialManager.BridgeMat;
        Cost = 2;
    }

    public override void Update()
    {
        name = "Bridge Tile. ID: " + Index;
        GetComponent<Renderer>().material = MaterialManager.BridgeMat;
    }
}