﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Grass : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Grass";
        GetComponent<Renderer>().material = MaterialManager.GrassMat;
        Cost = 4;
        Reset();
    }
    public override void Start()
    {
        Cost = 4;

    }

    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.GrassMat;
    }
}