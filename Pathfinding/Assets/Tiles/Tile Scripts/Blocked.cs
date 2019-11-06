using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Blocked : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.BlockedMat;
        Distance = 1.25f;
        name = "Blocked Tile. ID: " + Index;
        Cost = int.MaxValue;
        Reset();
    }

    public override void Start()
    {
        Walkable = false;
        Reset();
        GetComponent<Renderer>().material = MaterialManager.BlockedMat;
        Cost = int.MaxValue;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.BlockedMat;
        Cost = int.MaxValue;

        if (!Application.isPlaying)
            Reset();
    }
}
