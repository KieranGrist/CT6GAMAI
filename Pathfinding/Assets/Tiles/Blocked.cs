using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Blocked : TileNode
{
    public Color color;
    void Awake()
    {
        Distance = 1.25f;
        name = "Blocked";

        GetComponent<Renderer>().material = BlockedMat;
        Cost = float.PositiveInfinity;
        Walkable = false;
        Reset();
    }
    public override void Start()
    {
        name = "Blocked";
        Cost = float.PositiveInfinity;
        Walkable = false;

    }

    public override void Update()
    {
        GetComponent<Renderer>().material = BlockedMat;
    }
}
