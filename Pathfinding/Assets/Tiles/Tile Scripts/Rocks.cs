using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Rocks : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.RocksMat;
        Distance = 1.25f;
        name = "Residential Tile. ID: " + Index;
        Cost = 45;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.RocksMat;
        Cost = 45;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.RocksMat;
        Cost = 45;

        if (!Application.isPlaying)
            Reset();
    }
}
