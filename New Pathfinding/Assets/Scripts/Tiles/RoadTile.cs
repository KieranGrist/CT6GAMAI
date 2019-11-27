using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : Node
{
    private void Update()
    {
        gameObject.layer = 10;
    }
    public override void Reset()
    {
      

        Vector3 P = transform.position;
        gameObject.AddComponent<Rigidbody>();


        foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f), transform.rotation, LayerMask.GetMask("Road")))
        {
            transform.rotation = item.GetComponent<Road>().transform.rotation;
        }
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
            foreach (var item in Physics.OverlapBox(transform.position + transform.forward, new Vector3(transform.localScale.x - transform.localScale.x / 2, transform.localScale.y * 0.5f, transform.localScale.z - transform.localScale.z / 2)))
                if (item.transform.gameObject != gameObject && item.gameObject.GetComponent<Node>())
                    if (item.gameObject.GetComponent<Node>().Walkable)
                        hitObjects.Add(item);
            foreach (var item in hitObjects)
                Neighbours.Add(new Edge(this, item.gameObject.GetComponent<Node>()));
            DestroyImmediate(gameObject.GetComponent<Rigidbody>());
        }
        else
        {
            DestroyImmediate(GetComponent<Rigidbody>());
            DestroyImmediate(this);
        }
    }
}

