using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class TrainTracks : TileNode
{
    void Awake()
    {
        GetComponent<Renderer>().material = MaterialManager.TrainTracksMat;
        Distance = 1.25f;
        name = "Train Tracks Tile. ID: " + Index;
        Cost = 1;
        Reset();
    }

    public override void Start()
    {
        Reset();
        GetComponent<Renderer>().material = MaterialManager.TrainTracksMat;
        Cost = 1;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.TrainTracksMat;
        Cost = 1;

        if (!Application.isPlaying)
            Reset();
    }
}
