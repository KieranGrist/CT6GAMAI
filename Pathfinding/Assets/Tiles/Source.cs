using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Source : TileNode
{
    public Color color;
    void Awake()
    {
        Distance = 1.25f;
        name = "Source";
        Color test = Color.red;
        test = new Color(128, 0, 128);
        color = new Color(128, 0, 128);
    //    GetComponent<Renderer>().sharedMaterial.color = new Color(128, 0, 128);
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
        color = new Color(128, 0, 128);
    //    GetComponent<Renderer>().sharedMaterial.color = new Color(128, 0, 128);
    }
}
