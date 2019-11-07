using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class TrainStation : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.TrainStationMat;
        Cost = 2;
        name = "Train Station Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.TrainStationMat;
        Cost = 2;  
        name = "Train Station Tile. ID: " + Index;
     
           
    }
}
