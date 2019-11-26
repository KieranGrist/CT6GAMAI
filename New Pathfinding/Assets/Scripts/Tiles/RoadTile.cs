using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : Node
{
    public override void Reset()
    {
        Vector3 P = transform.position;
        gameObject.AddComponent<Rigidbody>();
        RB();
        transform.position = P;
        Cost = 5;
        name = "Node " + Index;
        foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f), transform.rotation, LayerMask.GetMask("Obstacle")))
            if (item.gameObject != gameObject)
                Walkable = false;
        Neighbours = new List<Edge>();
        Neighbours.Clear();
        List<Collider> hitObjects = new List<Collider>();
        if (Walkable)
        {
            foreach (var item in Physics.OverlapBox(transform.position, new Vector3(transform.localScale.x - transform.localScale.x / 2, transform.localScale.y * 0.5f, transform.localScale.z - transform.localScale.z / 2)))
                if (item.transform.gameObject != gameObject && item.gameObject.GetComponent<Node>())
                    if (item.gameObject.GetComponent<Node>().Walkable)
                        hitObjects.Add(item);
            foreach (var item in hitObjects)
                Neighbours.Add(new Edge(this, item.gameObject.GetComponent<Node>()));
        }
        else
        {
            DestroyImmediate(GetComponent<Rigidbody>());
            DestroyImmediate(this);
        }
      DestroyImmediate(gameObject.GetComponent<Rigidbody>());
    }
}

