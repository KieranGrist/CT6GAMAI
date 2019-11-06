using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Snow : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.SnowMat;
        Distance = 1.25f;
        name = "Snow Tile. ID: " + Index;
        Cost = 30;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.SnowMat;
        Cost = 30;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.SnowMat;
        Cost = 30;

        if (!Application.isPlaying)
            Reset();
    }
}
