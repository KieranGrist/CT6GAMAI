using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class SpacePort : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.SpacePortMat;
        Distance = 1.25f;
        name = "Space Port Tile. ID: " + Index;
        Cost = 15;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.SpacePortMat;
        Cost = 15;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.SpacePortMat;
        Cost = 15;

        if (!Application.isPlaying)
            Reset();
    }
}
