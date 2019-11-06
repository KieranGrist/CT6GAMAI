using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Lava : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.LavaMat;
        Distance = 1.25f;
        name = "Lava Tile. ID: " + Index;
        Cost = 50;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.LavaMat;
        Cost = 50;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.LavaMat;
        Cost = 50;

        if (!Application.isPlaying)
            Reset();
    }
}