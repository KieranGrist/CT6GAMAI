using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Mines : TileNode
{
  

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.MinesMat;
        Cost = 60;
        name = "Mines Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.MinesMat;
        Cost = 60;
        name = "Mines Tile. ID: " + Index;
     
           
    }
}