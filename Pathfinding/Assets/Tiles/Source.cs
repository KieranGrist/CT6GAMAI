using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Source : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Source";
        GetComponent<Renderer>().material = MaterialManager.SourceMat;
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
        GetComponent<Renderer>().material = MaterialManager.SourceMat;
    }
}
