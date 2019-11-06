using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Farm : TileNode
{
    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.FarmMat;
        Cost = 10;
        name = "Farm Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.FarmMat;
        Cost = 10;
        name = "Farm Tile. ID: " + Index;
     
           
    }
}
