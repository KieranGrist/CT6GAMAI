using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MilitaryAirport : TileNode
{
    public override void Reset()
    {
        Neighbours.Clear();
        List<Collider> hitObjects = new List<Collider>();
        foreach (var item in NavGraph.map.Nodes)    
            if (item != this)
                Neighbours.Add(new TileEdge(this, item));  
    
    }

    public  new void Start()
    {
        GetComponent<Renderer>().material = TileMaterials.Materials.MilitaryAirportMat;
        if (!Created)
        {
            GameObject go = Instantiate(TileMaterials.Materials.MilitaryAirportGameObject, transform);
            go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            go.transform.localScale = new Vector3(0.01f, 1, 0.01f);
            CreatedObject = true;
            TileGameObject = go;
            Created = true;
        }
        Cost = 2;
        name = "Millitary Airport Tile. ID: " + Index;          
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }
}