using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Blocked : TileNode
{
  
    public override void Start()
    {
        Walkable = false;
       
        GetComponent<Renderer>().material = MaterialManager.BlockedMat;
        Cost = int.MaxValue;
        name = "Blocked Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.BlockedMat;
        Cost = int.MaxValue;
        name = "Blocked Tile. ID: " + Index;
     
           
    }
}
