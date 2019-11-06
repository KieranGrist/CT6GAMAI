using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Ocean : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.OceanMat;
        Distance = 1.25f;
        name = "Ocean Tile. ID: " + Index;
        Cost = 60;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.OceanMat;
        Cost = 60;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.OceanMat;
        Cost = 60;

        if (!Application.isPlaying)
            Reset();
    }
}