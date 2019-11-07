using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Highway : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;
        Cost = 2;
        name = "Highway Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;
        Cost = 2;
        name = "Highway Tile. ID: " + Index;
     
           
    }
}