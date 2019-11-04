using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Highway : Tile
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Highway";
        GetComponent<Renderer>().sharedMaterial.color = new Color(255, 165, 0);
        Cost = 2;
        Reset();
    }
    public override void Start()
    {
        Cost = 2;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material.color = new Color(255, 165, 0);
    }
}
