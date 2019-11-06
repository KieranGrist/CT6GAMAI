using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Airport : TileNode
{
    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.AirportMat;
        Cost = 15;
        name = "Airport Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.AirportMat;
        Cost = 15;
        name = "Airport Tile. ID: " + Index;
     
           
    }
}
