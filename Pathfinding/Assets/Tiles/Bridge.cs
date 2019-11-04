using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Bridge : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Bridge";
        GetComponent<Renderer>().material = BridgeMat;
        Cost = 2;
        Reset();
    }
    public override void Start()
    {
        Cost = 2;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material = BridgeMat;
    }
}