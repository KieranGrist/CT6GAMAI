using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SpacePort : TileNode
{ 
    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.SpacePortMat;
        Cost = 15;
        name = "Space Port Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.SpacePortMat;
        Cost = 15;
        name = "Space Port Tile. ID: " + Index;
     
           
    }
}
