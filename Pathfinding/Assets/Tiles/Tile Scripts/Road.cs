using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Road : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.RoadMat;
        Distance = 1.25f;
        name = "Road Tile. ID: " + Index;
        Cost = 10;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.RoadMat;
        Cost = 10;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.RoadMat;
        Cost = 10;

        if (!Application.isPlaying)
            Reset();
    }
}