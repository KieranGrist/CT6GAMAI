using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Edge
{
    public Node From; //Where the edge is comming from
    public Node To; //Where the edge is going to 
    public Edge(Node From, Node To) //Constructor for edge which sets the from and two
    {
        this.From = From;
        this.To = To;
    }
    /// <summary>
    /// Gets the current distance of the edge
    /// </summary>
    /// <returns>
    /// Distance between the from node and the two node
    /// </returns>
    public float GetCost()
    {
        return Vector3.Distance(From.transform.position, To.transform.position);
    }
}