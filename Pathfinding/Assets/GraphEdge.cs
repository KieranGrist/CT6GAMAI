using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GraphEdge
{
    public GraphNode From;
    public GraphNode To;
    public GraphEdge(GraphNode From, GraphNode To)
    {
        this.From = From;
        this.To = To;
    }
}
