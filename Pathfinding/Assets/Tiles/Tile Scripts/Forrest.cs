using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Forrest : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.ForrestMat;
        Cost = 20;
        name = "Forrest Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.ForrestMat;
        Cost = 20;
        name = "Forrest Tile. ID: " + Index;
     
           
    }
}
