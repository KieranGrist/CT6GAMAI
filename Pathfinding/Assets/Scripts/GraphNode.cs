using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GraphNode : MonoBehaviour
{
    public int Index;
    public bool Walkable = true;
    public List<GraphEdge> AdjacencyList = new List<GraphEdge>();
    private void OnDrawGizmosSelected()
    {
        foreach (var item in AdjacencyList)
        {
            Gizmos.color = Color.blue;
            Vector3 TextLocation = new Vector3();
            TextLocation.x = item.From.transform.position.x + (item.To.transform.position.x - item.From.transform.position.x) / 2;
            TextLocation.y = item.From.transform.position.y + (item.To.transform.position.y - item.From.transform.position.y) / 2;
            TextLocation.z = item.From.transform.position.z + (item.To.transform.position.z - item.From.transform.position.z) / 2;

            //  Handles.Label(TextLocation, "Cost " + item.CumulativeCost);
            Gizmos.DrawLine(item.From.transform.position, item.To.transform.position);
        }
    }
    public GraphNode()
    {
        Walkable = true;
        Index = 0;
    }
    public GraphNode(bool Walkable = true)
    {
        this.Walkable = Walkable;         
    }
    public void Reset()
    {
        List<GraphNode> Nodes = new List<GraphNode>();
        Nodes.AddRange(FindObjectsOfType<GraphNode>());

        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i] != this)
            {

                if (Vector3.Distance(transform.position, Nodes[i].transform.position) < 2.25)
                {
                    AdjacencyList.Add(new GraphEdge(this, Nodes[i]));
                }
            }
        }
    }
     void Start()
    {
        Walkable = true;  
    }
    void Update()
    {
        if (!Walkable)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
