using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MilitaryAirport : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.MilitaryAirportMat;
        Cost = 55;
        name = "Millitary Airport Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.MilitaryAirportMat;
        Cost = 55;
        name = "Millitary Airport Tile. ID: " + Index;
     
           
    }
}