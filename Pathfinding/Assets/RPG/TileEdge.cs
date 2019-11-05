using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TileEdge 
{
    public TileNode From;
    public TileNode To;
    public TileEdge(TileNode From, TileNode To)
    {
        this.From = From;
        this.To = To;
    }
    public int GetCost()
    {
        return From.Cost + To.Cost;
    }
}
