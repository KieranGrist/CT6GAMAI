﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Empty : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Empty Tile. ID: " + Index;
  
        Cost = int.MaxValue;
        Walkable = false;
        Reset();
    }
    public override void Start()
    {
        GetComponent<Renderer>().material = MaterialManager.EmptyMat;
        Cost = int.MaxValue;
        Walkable = false;
    }

    public override void Update()
    {
        name = "Empty Tile. ID: " + Index;
        GetComponent<Renderer>().material = MaterialManager.EmptyMat;
    }
}
