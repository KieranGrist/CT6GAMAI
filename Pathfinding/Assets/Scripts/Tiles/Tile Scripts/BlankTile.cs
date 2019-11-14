using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankTile : TileNode
{
    public override void Reset()
    {
        Distance = 1.25f;
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

    }
    new void Start()
    {
        name = "Blank Tile " + Index;
        Reset();
    }
}

