using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Bridge : Tile
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Bridge";
        GetComponent<Renderer>().sharedMaterial.color = new Color(255, 127, 80,1);
        Cost = 2;
        Reset();
    }
    public override void Start()
    {
        Cost = 2;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material.color = new Color(255, 127, 80);
    }
}