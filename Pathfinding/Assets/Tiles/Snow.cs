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
        GetComponent<Renderer>().material = MaterialManager.SnowMat;
        Cost = 9;
        Reset();
    }
    public override void Start()
    {
        Cost = 9;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.SnowMat;
        Cost = 9;
    }
}
