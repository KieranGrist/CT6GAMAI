using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Highway : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Highway";

        GetComponent<Renderer>().material = MaterialManager.HighwayMat;
        Cost = 2;
        Reset();
    }
    public override void Start()
    {
        Cost = 2;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;

    }
}
