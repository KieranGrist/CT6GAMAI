using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Power : TileNode
{


    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.PowerMat;
        Cost = 15;
        name = "Power Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.PowerMat;
        Cost = 15;
        name = "Power Tile. ID: " + Index;
     
           
    }
}