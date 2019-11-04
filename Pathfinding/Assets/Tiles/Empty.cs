using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Empty : Tile
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Empty";
        GetComponent<Renderer>().sharedMaterial.color = Color.black;
        Cost = float.PositiveInfinity;
        Walkable = false;
        Reset();
    }
    public override void Start()
    {
        Cost = float.PositiveInfinity;
        Walkable = false;  
    }

    public override void Update()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }
}
