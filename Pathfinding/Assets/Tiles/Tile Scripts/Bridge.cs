using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Bridge : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.BridgeMat;
        Distance = 1.25f;
        name = "Bridge Tile. ID: " + Index;
        Cost = 5;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.BridgeMat;
        Cost = 5;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.BridgeMat;
        Cost = 5; 

        if (!Application.isPlaying)
            Reset();
    }
}