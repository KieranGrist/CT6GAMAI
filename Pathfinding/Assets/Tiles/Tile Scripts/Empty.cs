using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Empty : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.EmptyMat;
        Cost = int.MaxValue;
        name = "Empty Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.EmptyMat;
        Cost = int.MaxValue;
        name = "Empty Tile. ID: " + Index;
     
           
    }
}
