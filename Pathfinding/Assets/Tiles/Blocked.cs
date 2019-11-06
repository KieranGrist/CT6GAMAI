using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Blocked : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Blocked Tile. ID: " + Index;
        
        Cost = int.MaxValue;
        Walkable = false;
        Reset();
    }
    public override void Start()
    {
        GetComponent<Renderer>().material = MaterialManager.BlockedMat;

        Cost = int.MaxValue;
        Walkable = false;

    }

    public override void Update()
    {
        name = "Blocked Tile. ID: " + Index;
        GetComponent<Renderer>().material = MaterialManager.BlockedMat;
    }
}
