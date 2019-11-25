using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Node : MonoBehaviour
{
    public List<Edge> Neighbours = new List<Edge>();
    public float Cost = float.PositiveInfinity;
    public float Distance = 2;
    public int Index;
    public bool Walkable = true;
    Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
        rb.freezeRotation = true;
        gameObject.layer = LayerMask.GetMask("Node");
        Walkable = true;
        //physics .check box if something is above it not walkable 
        //Physics.CheckBox()
    }
    // Update is called once per frame
    void Update()
    {
        if (Walkable)
            GetComponent<Renderer>().material.color = Color.green;
        else
            GetComponent<Renderer>().material.color = Color.red;
        name = "Node " + Index;
    }
    public Node(int Index, bool Walkable)
    {
        this.Index = Index;
        this.Walkable = Walkable;
        Neighbours = new List<Edge>();
    }
    public void Reset()
    { 
        name = "Node " + Index;
        foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 1, 0), transform.localScale, transform.rotation, LayerMask.GetMask("Obstacle")))
            if (item.gameObject != gameObject)
                Walkable = false;
        Neighbours = new List<Edge>();
        Neighbours.Clear();
        List<Collider> hitObjects = new List<Collider>();
        foreach (var item in Physics.OverlapBox(transform.position, new Vector3((transform.localScale.x * 2) /2 - 0.2f , transform.localScale.y / 2 , (transform.localScale.z * 2) / 2 - 0.2f)))
            if (item.transform.gameObject != gameObject && item.gameObject.GetComponent<Node>())
                hitObjects.Add(item);             
        foreach (var item in  hitObjects)   
            Neighbours.Add(new Edge(this, item.gameObject.GetComponent<Node>()));
   
    }
}
