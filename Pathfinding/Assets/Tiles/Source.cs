using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Source : Tile
{
    void Awake()
    {
        Distance = 1.25f;

        name = "Source";
        GetComponent<Renderer>().sharedMaterial.color = new Color(128, 0, 128);
        Cost = 0;
        Reset();
    }
    public override void Start()
    {
         Cost = 0;
        Reset();
    }

    public override void Update()
    {
        GetComponent<Renderer>().material.color = new Color(128, 0, 128);
    }
}
