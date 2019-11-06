using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Highway : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;
        Distance = 1.25f;
        name = "Highway Tile. ID: " + Index;
        Cost = 2;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;
        Cost = 2;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;
        Cost = 2;

        if (!Application.isPlaying)
            Reset();
    }
}