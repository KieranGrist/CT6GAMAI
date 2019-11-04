using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Water : Tile
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Water";
        GetComponent<Renderer>().sharedMaterial.color = Color.blue;
        Cost = 10;
        Reset();
    }

    public override void Start()
    {
        Cost = 10;
        Reset();
    }

    public override void Update()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
