using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Water : TileNode
{
    public Color color;
    void Awake()
    {
        Distance = 1.25f;
        name = "Water";
        color = Color.blue;     
    //    GetComponent<Renderer>().sharedMaterial.color = Color.blue;
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
        color = Color.blue;
    //    GetComponent<Renderer>().sharedMaterial.color = Color.blue;
    }
}
