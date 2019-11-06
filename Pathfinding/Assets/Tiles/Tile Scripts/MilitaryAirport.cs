using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class MilitaryAirport : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.MilitaryAirportMat;
        Distance = 1.25f;
        name = "Millitary Airport Tile. ID: " + Index;
        Cost = 55;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.MilitaryAirportMat;
        Cost = 55;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.MilitaryAirportMat;
        Cost = 55;

        if (!Application.isPlaying)
            Reset();
    }
}