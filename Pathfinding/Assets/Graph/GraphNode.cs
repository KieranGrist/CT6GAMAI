using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GraphNode : MonoBehaviour
{
    public float Distance = 2.25f;
    public int Index;
    public List<GraphEdge> Neighbours = new List<GraphEdge>();
    public bool Walkable;
    private void OnDrawGizmosSelected()
    {
        foreach (var item in Neighbours)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(item.From.transform.position, item.To.transform.position);
        }
    }
    private void Start()
    {
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<GraphNode>();
        }
    }
    public GraphNode()
    {
        Index = 0;
        Walkable = true;
        Neighbours = new List<GraphEdge>();
    }
    public GraphNode(int Index, bool Walkable)
    {
        this.Index = Index;
        this.Walkable = Walkable;
        Neighbours = new List<GraphEdge>();
    }
    public GraphNode(int Index, bool Walkable, Vector2 Position)
    {
        this.Index = Index;
        this.Walkable = Walkable;
    }
    public void Reset()
    {
        List<GraphNode> Nodes = new List<GraphNode>();
        Neighbours.Clear();
        Nodes.AddRange(FindObjectsOfType<GraphNode>());

        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i] != this)
            {

                if (Vector3.Distance(transform.position, Nodes[i].transform.position) < Distance)
                {
                    Neighbours.Add(new GraphEdge(this, Nodes[i]));
                }
            }
        }
    }
    void Update()
    {
        if (!Walkable)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}