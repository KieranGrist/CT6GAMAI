using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class River : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.RiverMat;
        Cost = 20;
        name = "River Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.RiverMat;
        Cost = 20;
        name = "River Tile. ID: " + Index;
     
           
    }
}