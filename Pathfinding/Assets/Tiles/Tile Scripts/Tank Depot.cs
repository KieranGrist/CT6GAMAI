using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class TankDepot : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.TankDepotMat;
        Cost = 15;
        name = "Tank Depot Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.TankDepotMat;
        Cost = 15;
        name = "Tank Depot Tile. ID: " + Index;
     
           
    }
}

