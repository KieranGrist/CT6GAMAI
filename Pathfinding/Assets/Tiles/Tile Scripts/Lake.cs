using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Lake : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.LakeMat;
        Cost = 15;
        name = "Lake Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.LakeMat;
        Cost = 15;
        name = "Lake Tile. ID: " + Index;
     
           
    }
}