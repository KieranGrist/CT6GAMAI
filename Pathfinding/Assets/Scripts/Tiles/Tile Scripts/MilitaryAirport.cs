using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class MilitaryAirport : TileNode
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

        List<MilitaryAirport> militaryAirports = new List<MilitaryAirport>();
        militaryAirports.AddRange(FindObjectsOfType<MilitaryAirport>());
        foreach (var item in militaryAirports)
            if (item != this)
                Neighbours.Add(new TileEdge(GetComponent<TileNode>(), item));

        foreach (var item in Neighbours)
        {
          //  item.To.Reset();
        }
    
    }

    public  new void Start()
    {
        NeedToReset = true;
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
        if (NeedToReset)
        {
            Reset();
            NeedToReset = false;
        }
    }
}