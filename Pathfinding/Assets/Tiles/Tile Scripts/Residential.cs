using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Residential : TileNode
{

    public override void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.ResidentialMat;
        Cost = 15;
        name = "Residential Tile. ID: " + Index;
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public override void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.ResidentialMat;
        Cost = 15;
        name = "Residential Tile. ID: " + Index;
     
           
    }
}