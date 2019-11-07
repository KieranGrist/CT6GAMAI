using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Road : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.RoadMat;
        Cost = 10;
        name = "Road Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.RoadMat;
        Cost = 10;
        name = "Road Tile. ID: " + Index;
     
           
    }
}