using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstras : Pathfinder
{
    public Dijkstras(GraphMap navGraph) : base(navGraph)
    {

    }
    public override bool CalculateRoute(GraphNode Source, GraphNode Target)
    {
        throw new System.NotImplementedException();
    }

    public override bool CalculateRoute(TileNode Source, TileNode Target)
    {
        Cost[Source.Index] = 0;
        bool TargetNodeFound = false;
        List<TileEdge> TraveresedEdges = new List<TileEdge>(Graph.Nodes.Count);
        PriorityQueue<int, TileEdge> MinPriorityQueue = new PriorityQueue<int, TileEdge>();
        TileReset();
        for (int i =0; i < Source.Neighbours.Count; i++)
        {
            KeyValuePair<int, TileEdge> keyValuePair = new KeyValuePair<int, TileEdge>(Source.Neighbours[i].GetCost(), Source.Neighbours[i]);
            MinPriorityQueue.Enqueue(keyValuePair);
        }
        while(MinPriorityQueue.Count() >  0)
        {
            KeyValuePair<int, TileEdge> keyValuePair = MinPriorityQueue.Dequeue();
            TileEdge Edge = keyValuePair.Value;
            
        }
        return false;
    }
}
