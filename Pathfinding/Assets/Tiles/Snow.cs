using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Snow : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Snow";
        GetComponent<Renderer>().material = SourceMat;
        Cost = 2;
        Reset();
    }
    public override void Start()
    {
        Cost = 2;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material = SourceMat;
        Cost = 2;
    }
}
