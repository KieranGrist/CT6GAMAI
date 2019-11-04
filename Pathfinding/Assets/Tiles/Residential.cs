using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Residential : Tile
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Residential";
        GetComponent<Renderer>().sharedMaterial.color = new Color(255, 192, 203);
        Cost = 6;
        Reset();
    }
    public override void Start()
    {
        Cost = 6;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material.color = new Color(255, 192, 203);
    }
}
