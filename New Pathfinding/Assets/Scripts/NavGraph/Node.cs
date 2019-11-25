using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Edge> Neighbours = new List<Edge>();
    public float Cost = float.PositiveInfinity;
    public float Distance = 2;
    public int Index;
    public bool Walkable = true;

    // Start is called before the first frame update
    void Start()
    {
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
    private void OnDrawGizmos()
    {       
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 1, 0), new Vector3(1, 1, 1));
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0, 0), new Vector3(2.8f, 0, 2.8f));
    }
    public void Reset()
    {
        name = "Node " + Index;
        Debug.Log(name + " Debug log");
        foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 1, 0), new Vector3(1, 1, 1), transform.rotation, LayerMask.GetMask("Obstacle")))
        {
            if (item.gameObject != gameObject)
                Walkable = false;
            Debug.Log(item.name);
        }


        Neighbours.Clear();
        List<Collider> hitObjects = new List<Collider>();
        foreach (var item in Physics.OverlapSphere(transform.position, Distance))
        {
            if (item.transform.gameObject != gameObject && item.GetComponent<Node>())
                hitObjects.Add(item);
        }  
        int i = 0;
        foreach (var item in  hitObjects)
        {
            Neighbours.Add(new Edge(GetComponent<Node>(), hitObjects[i].gameObject.GetComponent<Node>()));
            i++;
        }
    }
}
