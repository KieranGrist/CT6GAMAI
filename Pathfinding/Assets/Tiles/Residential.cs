using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Residential : TileNode
{
    public Color color;
    void Awake()
    {
        Distance = 1.25f;
        name = "Residential";
        Color test = new Color();
           test = Color.red;
        test = new Color(255, 20, 147);
        color = new Color(255, 20, 147);
    //    GetComponent<Renderer>().sharedMaterial.color = test;
        Cost = 6;
        Reset();
    }
    public override void Start()
    {
        Cost = 6;
    }

    public override void Update()
    {
        color = new Color(255, 20, 147);
    //    GetComponent<Renderer>().sharedMaterial.color = new Color(255, 20, 147);
    }
}
