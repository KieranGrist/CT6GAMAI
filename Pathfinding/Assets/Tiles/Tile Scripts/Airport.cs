﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Airport : TileNode
{
    public override void Reset()
    {
        Neighbours.Clear();
        List<Collider> hitObjects = new List<Collider>();
        foreach (var item in Physics.OverlapSphere(transform.position, Distance))
        {
            if (item.transform.gameObject != gameObject && item.GetComponent<TileNode>())
                hitObjects.Add(item);
        }
        gameObjects.Clear();
        int i = 0;
        while (i < hitObjects.Count)
        {
            gameObjects.Add(hitObjects[i].transform.gameObject);
            Neighbours.Add(new TileEdge(GetComponent<TileNode>(), hitObjects[i].gameObject.GetComponent<TileNode>()));
            i++;
        }
        List<Airport> airports = new List<Airport>();
        airports.AddRange(FindObjectsOfType<Airport>());
        foreach (var item in airports)
            if (item != this)
                Neighbours.Add(new TileEdge(GetComponent<TileNode>(), item));
    }
    public  new void Start()
    {
        
        GetComponent<Renderer>().material = MaterialManager.AirportMat;
        if (!Created)
        {
            GameObject go = Instantiate(MaterialManager.AirportGameObject,transform);
        go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        go.transform.localScale = new Vector3(0.01f, 1, 0.01f);
        CreatedObject = true;
        TileGameObject = go;
            Created = true;
        }
        Cost = 15;
        name = "Airport Tile. ID: " + Index;          
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public  new void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.AirportMat;
        Cost = 15;
        name = "Airport Tile. ID: " + Index;         
        if (NeedToReset)
        {
            Reset();
            NeedToReset = false;
        }
           
    }
}
