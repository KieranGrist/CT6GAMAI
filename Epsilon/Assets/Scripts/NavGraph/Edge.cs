﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Edge
{
    public Node From;
    public Node To;
    public Edge(Node From, Node To)
    {
        this.From = From;
        this.To = To;
    }
   public float GetCost()
    {
        return this.From.Cost + this.To.Cost;
    }
}