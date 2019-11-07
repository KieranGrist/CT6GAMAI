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
        TileReset();
 
        TargetNodeFound = false;
        List<TileEdge> TraveresedEdges = new List<TileEdge>();
        PriorityQueue<int, TileEdge> MinPriorityQueue = new PriorityQueue<int, TileEdge>();

        Cost[Source.Index] = 0;
        for (int i = 0; i < Source.Neighbours.Count; i++)
        {
            KeyValuePair<int, TileEdge> keyValuePair = new KeyValuePair<int, TileEdge>(0, Source.Neighbours[i]);
            MinPriorityQueue.Enqueue(keyValuePair);
        }
        while (MinPriorityQueue.Count() > 0)
        {
            KeyValuePair<int, TileEdge> keyValuePair = MinPriorityQueue.Dequeue();
            TraveresedEdges.Add(keyValuePair.Value);
            TileEdge Edge = keyValuePair.Value;
            if (Cost[Edge.To.Index] > Cost[Edge.From.Index] + Edge.GetCost())
            {
                Route[Edge.To.Index] = Edge.From.Index;
                Cost[Edge.To.Index] = Cost[Edge.From.Index] + Edge.GetCost();
            }
            if (Edge.To.Index == Target.Index)
            {
                TargetNodeFound = true;
            }
            for (int i = 0; i < Edge.To.Neighbours.Count; i++)
            {
                KeyValuePair<int, TileEdge> valuepair = new KeyValuePair<int, TileEdge>(Edge.To.Neighbours[i].GetCost() + Cost[Edge.To.Index], Edge.To.Neighbours[i]);
                if (Edge.To.Walkable && !TraveresedEdges.Contains(Edge.To.Neighbours[i]) && !MinPriorityQueue.data.Contains(valuepair))
                {
                    MinPriorityQueue.Enqueue(valuepair);
                }
            }
        }
        if (TargetNodeFound)
            GeneratedPath = CalculatePath(Source, Target);
        return TargetNodeFound;
    }
}
