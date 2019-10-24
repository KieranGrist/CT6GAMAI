using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GraphEdge
{
    public GraphNode From;
    public GraphNode To;
    public float CumulativeCost;
    public int ID;
    public GraphEdge(GraphNode From, GraphNode To, float Cost)
    {
        this.From = From;
        this.To = To;
        this.CumulativeCost = Cost;
     

    }

    public GraphEdge()
    {
   
    }
}