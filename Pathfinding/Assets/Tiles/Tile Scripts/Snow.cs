using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Snow : TileNode
{
  
    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.SnowMat;
        Cost = 30;
        name = "Snow Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.SnowMat;
        Cost = 30;
        name = "Snow Tile. ID: " + Index;
     
           
    }
}
