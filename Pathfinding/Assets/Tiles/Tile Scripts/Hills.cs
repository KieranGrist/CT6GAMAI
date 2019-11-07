using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Hills : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.HillsMat;
        Cost = 20;
        name = "Hills Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.HillsMat;
        Cost = 20;
        name = "Hills Tile. ID: " + Index;
     
           
    }
}