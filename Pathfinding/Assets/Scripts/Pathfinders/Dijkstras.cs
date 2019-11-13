using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Dijkstras : Pathfinder
{
    void Start()
    {
        Route = new List<int>();
        Cost = new List<float>();
        Visited = new List<bool>();
        GeneratedPath = new List<int>();
        Route.Add(0);
        Cost.Add(0);
        Visited.Add(false);
        GeneratedPath.Add(0);
    }

    public override bool CalculateRoute(AIAgent ARTIE, TileNode Source, TileNode Target)
    {
        DateTime StartTime = DateTime.Now;
        TileReset();
 
        TargetNodeFound = false;
        List<TileEdge> TraveresedEdges = new List<TileEdge>();
        PriorityQueue<float, TileEdge> MinPriorityQueue = new PriorityQueue<float, TileEdge>();

        Cost[Source.Index] = 0;
        for (int i = 0; i < Source.Neighbours.Count; i++)
        {
            KeyValuePair<float, TileEdge> keyValuePair = new KeyValuePair<float, TileEdge>(0, Source.Neighbours[i]);
            MinPriorityQueue.Enqueue(keyValuePair);
        }
        while (MinPriorityQueue.Count() > 0)
        {
            KeyValuePair<float, TileEdge> keyValuePair = MinPriorityQueue.Dequeue();
            TraveresedEdges.Add(keyValuePair.Value);
            TileEdge Edge = keyValuePair.Value;
            if (Cost[Edge.To.Index] > Cost[Edge.From.Index] + Edge.GetCost())
            {
                Route[Edge.To.Index] = Edge.From.Index;
                Cost[Edge.To.Index] = Cost[Edge.From.Index] + Edge.GetCost();
            }
            if (Edge.To.Index == Target.Index)        
                TargetNodeFound = true;       
            for (int i = 0; i < Edge.To.Neighbours.Count; i++)
            {
                KeyValuePair<float, TileEdge> valuepair = new KeyValuePair<float, TileEdge>(Edge.To.Neighbours[i].GetCost() + Cost[Edge.To.Index], Edge.To.Neighbours[i]);
                if (Edge.To.Walkable && !TraveresedEdges.Contains(Edge.To.Neighbours[i]) && !MinPriorityQueue.data.Contains(valuepair))
                    MinPriorityQueue.Enqueue(valuepair);              
            }
        }
        TimeCalculated = DateTime.Now - StartTime;
        FunctionTime = (float)TimeCalculated.TotalMilliseconds;
        if (TargetNodeFound)
            GeneratedPath = CalculatePath(Source, Target);
        return TargetNodeFound;
    }
}
