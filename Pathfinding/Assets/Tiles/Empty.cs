using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Empty : TileNode
{
    public Color color;
    void Awake()
    {
        Distance = 1.25f;
        name = "Empty";
        color = Color.black;
    //    GetComponent<Renderer>().sharedMaterial.color = Color.black;
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
    //    GetComponent<Renderer>().sharedMaterial.color = Color.black;
    }
}
