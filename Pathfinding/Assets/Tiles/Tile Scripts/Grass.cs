using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Grass : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.GrassMat;
        Distance = 1.25f;
        name = "Grass Tile. ID: " + Index;
        Cost = 5;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.GrassMat;
        Cost = 5;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.GrassMat;
        Cost = 5;

        if (!Application.isPlaying)
            Reset();
    }
}
