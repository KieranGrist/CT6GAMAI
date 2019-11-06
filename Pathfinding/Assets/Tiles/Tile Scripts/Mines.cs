using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Mines : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.MinesMat;
        Distance = 1.25f;
        name = "Mines Tile. ID: " + Index;
        Cost = 60;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.MinesMat;
        Cost = 60;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.MinesMat;
        Cost = 60;

        if (!Application.isPlaying)
            Reset();
    }
}