using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MilitaryBase : TileNode
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
        Military = true;
    }

    public  new void Start()
    {
        GetComponent<Renderer>().material = TileMaterials.Materials.MillitaryBaseMat;
        if (!Created)
        {
            GameObject go = Instantiate(TileMaterials.Materials.MillitaryBaseGameObject, transform);
            go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            go.transform.localScale = new Vector3(1, 1, 1);
            CreatedObject = true;
            TileGameObject = go;
            Created = true;
        }
        Cost = 15;
        name = "Military Base Tile. ID: " + Index;          
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }
}

