using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MilitaryBase : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.MillitaryBaseMat;
        Cost = 15;
        name = "Military Base Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.MillitaryBaseMat;
        Cost = 15;
        name = "Tank Depot Tile. ID: " + Index;
     
           
    }
}

