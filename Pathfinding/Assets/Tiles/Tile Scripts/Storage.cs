using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Storage : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.StorageMat;
        Distance = 1.25f;
        name = "Storage Tile. ID: " + Index;
        Cost = 15;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.StorageMat;
        Cost = 15;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.StorageMat;
        Cost = 15;

        if (!Application.isPlaying)
            Reset();
    }
}
