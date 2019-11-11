﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Grass : TileNode
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
        foreach (var item in Neighbours)
        {
         //   item.To.NeedToReset = true;
        }
    }

    public  new void Start()
    {
        NeedToReset = true;
        GetComponent<Renderer>().material = MaterialManager.GrassMat;
        if (!Created)
        {
            GameObject go = Instantiate(MaterialManager.EmptyGameObject, transform);
            go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            go.transform.localScale = new Vector3(1, 1, 1);
            CreatedObject = true;
            TileGameObject = go;
            Created = true;
        }
        Cost = 5;
        name = "Grass Tile. ID: " + Index;          
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public  new void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.GrassMat;
        Cost = 5;
        name = "Grass Tile. ID: " + Index;
        if (NeedToReset)
        {
            Reset();
            NeedToReset = false;
        }

    }
}
