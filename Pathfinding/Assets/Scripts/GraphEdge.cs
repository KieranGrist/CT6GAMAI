using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GraphEdge
{
    public GraphNode From, To;
    // Start is called before the first frame update
    public GraphEdge(GraphNode From, GraphNode To)
    {
        this.From = From;
        this.To = To;
    }
}