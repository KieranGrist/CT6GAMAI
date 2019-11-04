using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Snow : TileNode
{
    public Color color;
    void Awake()
    {
        Distance = 1.25f;
        name = "Snow";
    //    GetComponent<Renderer>().sharedMaterial.color = Color.white;
        Cost = 2;
        Reset();
    }
    public override void Start()
    {
        Cost = 2;
    }

    public override void Update()
    {
        color = Color.white;
    //    GetComponent<Renderer>().sharedMaterial.color = Color.white;
    }
}
