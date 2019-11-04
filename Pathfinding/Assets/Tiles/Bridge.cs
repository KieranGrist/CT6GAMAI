using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Bridge : TileNode
{
    public Color color;
    void Awake()
    {
        Distance = 1.25f;
        name = "Bridge";
        color = new Color(255, 127, 80);
    //    GetComponent<Renderer>().sharedMaterial.color = new Color(255, 127, 80);
        Cost = 2;
        Reset();
    }
    public override void Start()
    {
        Cost = 2;
    }

    public override void Update()
    {
        color = new Color(255, 127, 80);
    //    GetComponent<Renderer>().sharedMaterial.color = new Color(255, 127, 80);
    }
}