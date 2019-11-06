using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class TrainStation : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.TrainStationMat;
        Distance = 1.25f;
        name = "Train Station Tile. ID: " + Index;
        Cost = 2;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.TrainStationMat;
        Cost = 2;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.TrainStationMat;
        Cost = 2;

        if (!Application.isPlaying)
            Reset();
    }
}
