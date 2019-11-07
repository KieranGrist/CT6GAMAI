using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Jungle : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.JungleMat;
        Cost = 40;
        name = "Jungle Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.JungleMat;
        Cost = 40;
        name = "Jungle Tile. ID: " + Index;
     
           
    }
}