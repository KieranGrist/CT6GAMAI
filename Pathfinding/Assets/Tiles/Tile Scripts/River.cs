using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class River : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.RiverMat;
        Distance = 1.25f;
        name = "River Tile. ID: " + Index;
        Cost = 20;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.RiverMat;
        Cost = 20;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.RiverMat;
        Cost = 20;

        if (!Application.isPlaying)
            Reset();
    }
}