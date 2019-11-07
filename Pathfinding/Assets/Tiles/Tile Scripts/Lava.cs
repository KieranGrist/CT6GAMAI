﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Lava : TileNode
{



    public override void Start()
    {
        GetComponent<Renderer>().material = MaterialManager.LavaMat;
        Cost = 50;
        name = "Lava Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.LavaMat;
        Cost = 50;
        name = "Lava Tile. ID: " + Index; 
    }
}