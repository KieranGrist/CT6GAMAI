using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class TrainTracks : TileNode
{
  

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.TrainTracksMat;
        Cost = 1;
        name = "Train Tracks Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.TrainTracksMat;
        Cost = 1;
        name = "Train Tracks Tile. ID: " + Index;
     
           
    }
}
