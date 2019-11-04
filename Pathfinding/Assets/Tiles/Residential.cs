using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Residential : TileNode
{
    void Awake()
    {
        Distance = 1.25f;
        name = "Residential";
        GetComponent<Renderer>().material = ResidentialMat;
        Cost = 6;
        Reset();
    }
    public override void Start()
    {
        Cost = 6;
    }

    public override void Update()
    {
        GetComponent<Renderer>().material = ResidentialMat;
    }
}
