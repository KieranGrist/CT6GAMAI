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
        name = "Empty";
        GetComponent<Renderer>().material = MaterialManager.EmptyMat;
        Cost = int.MaxValue;
        Walkable = false;
        Reset();
    }
    public override void Start()
    {
        Cost = int.MaxValue;
        Walkable = false;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.EmptyMat;
    }
}