using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MilitaryAirport : TileNode
{
    public override void Reset()
    {
        Military = true;
        Neighbours.Clear();
        List<Collider> hitObjects = new List<Collider>();
        HashSet<TileNode> AddedNodes = new HashSet<TileNode>();
        foreach (var item in Physics.OverlapSphere(transform.position, Distance))
        {
            if (item.transform.gameObject != gameObject && item.GetComponent<TileNode>())
                hitObjects.Add(item);
        }
        foreach (var item in hitObjects)
        {
            if (item != this)
            {
                gameObjects.Add(item.transform.gameObject);
                Neighbours.Add(new TileEdge(GetComponent<TileNode>(), item.gameObject.GetComponent<TileNode>()));
                AddedNodes.Add(item.gameObject.GetComponent<TileNode>());
            }

        }
        List<Airport> airports = new List<Airport>();
        airports.AddRange(FindObjectsOfType<Airport>());
        foreach (var item in airports)
            if (item != this || !AddedNodes.Contains(item))
            {
                Neighbours.Add(new TileEdge(this, item));
                AddedNodes.Add(item);
            }
        List<MilitaryAirport> MilitaryAirports = new List<MilitaryAirport>();
        MilitaryAirports.AddRange(FindObjectsOfType<MilitaryAirport>());
        foreach (var item in MilitaryAirports )
            if (item != this || !AddedNodes.Contains(item))
            {
                Neighbours.Add(new TileEdge(this, item));
                AddedNodes.Add(item);
            }
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
        Cost = 1;
        name = "Millitary Airport Tile. ID: " + Index;          
        foreach (var item in Neighbours)      
            item.From = GetComponent<TileNode>();        
    }
}