using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Path : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.PathMat;
        Distance = 1.25f;
        name = "Path Tile. ID: " + Index;
        Cost = 12;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.PathMat;
        Cost = 12;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.PathMat;
        Cost = 12;

        if (!Application.isPlaying)
            Reset();
    }
}