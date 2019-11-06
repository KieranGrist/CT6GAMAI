using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Empty : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.EmptyMat;
        Distance = 1.25f;
        name = "Empty Tile. ID: " + Index;
        Cost = int.MaxValue;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.EmptyMat;
        Cost = int.MaxValue;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.EmptyMat;
        Cost = int.MaxValue;

        if (!Application.isPlaying)
            Reset();
    }
}
