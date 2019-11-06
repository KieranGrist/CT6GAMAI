using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Jungle : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.JungleMat;
        Distance = 1.25f;
        name = "Jungle Tile. ID: " + Index;
        Cost = 40;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.JungleMat;
        Cost = 40;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.JungleMat;
        Cost = 40;

        if (!Application.isPlaying)
            Reset();
    }
}