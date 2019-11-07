using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Ocean : TileNode
{


    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.OceanMat;
        Cost = 60;
        name = "Ocean Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.OceanMat;
        Cost = 60;
        name = "Ocean Tile. ID: " + Index;
     
           
    }
}