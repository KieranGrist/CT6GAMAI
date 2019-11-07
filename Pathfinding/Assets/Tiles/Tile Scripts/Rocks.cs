using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Rocks : TileNode
{


    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.RocksMat;
        Cost = 45;
        name = "Rocks Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.RocksMat;
        Cost = 45;
        name = "Rocks Tile. ID: " + Index;
     
           
    }
}
