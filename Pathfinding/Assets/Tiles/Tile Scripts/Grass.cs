using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Grass : TileNode
{
  

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.GrassMat;
        Cost = 5;
        name = "Grass Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.GrassMat;
        Cost = 5;
        name = "Grass Tile. ID: " + Index;
     
           
    }
}
