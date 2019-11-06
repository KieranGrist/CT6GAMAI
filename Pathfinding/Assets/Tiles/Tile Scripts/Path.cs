using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Path : TileNode
{
  

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.PathMat;
        Cost = 12;
        name = "Path Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.PathMat;
        Cost = 12;
        name = "Path Tile. ID: " + Index;
     
           
    }
}