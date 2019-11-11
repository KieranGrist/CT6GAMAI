﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class MilitaryAirport : TileNode
{
    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public  new void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.MilitaryAirportMat;
        if (!Created)
        {
            GameObject go = Instantiate(MaterialManager.MilitaryAirportGameObject, transform);
            go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            go.transform.localScale = new Vector3(0.01f, 1, 0.01f);
            CreatedObject = true;
            TileGameObject = go;
            Created = true;
        }
        Cost = 55;
        name = "Millitary Airport Tile. ID: " + Index;          
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public  new void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.MilitaryAirportMat;
        Cost = 55;
        name = "Millitary Airport Tile. ID: " + Index;
        if (NeedToReset)
        {
            Reset();
            NeedToReset = false;
        }
    }
}