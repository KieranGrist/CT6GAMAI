using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ASTAR : Pathfinder
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
    float CalculateCost(TileEdge Edge, TileNode Target)
    {
        float G = Cost[Edge.From.Index] + Edge.GetCost();
        float X = (Mathf.Abs(Edge.From.transform.position.x - Target.transform.position.x)) / 100;
        float Z = (Mathf.Abs(Edge.From.transform.position.z - Target.transform.position.z)) / 100;
        float H = X + Z;
        float F = G + H;
        return F;
    }
    public override bool CalculateRoute(AIAgent ARTIE, TileNode Source, TileNode Target)
    {
        DateTime StartTime = DateTime.Now;
        TileReset();
        TargetNodeFound = false;
        HashSet<TileEdge> TraveresedEdges = new HashSet<TileEdge>();
        PriorityQueue<float, TileEdge> MinPriorityQueue = new PriorityQueue<float, TileEdge>();
        Cost[Source.Index] = 0;
        foreach (var item in Source.Neighbours)
        {
            KeyValuePair<float, TileEdge> keyValuePair = new KeyValuePair<float, TileEdge>(0, item);
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
                Cost[Edge.To.Index] = CalculateCost(Edge, Target);
            }
            if (    Edge.To.Military && !ARTIE.Military)
                Cost[Edge.To.Index] = float.PositiveInfinity;
            if (Edge.To.Index == Target.Index && !TargetNodeFound)
            {
                TargetNodeFound = true;
            }
            foreach (var item in Edge.To.Neighbours)
            {
                float G = item.GetCost() + Cost[Edge.To.Index];
                float X = (Mathf.Abs(Edge.From.transform.position.x - Target.transform.position.x)) / 100;
                float Z = (Mathf.Abs(Edge.From.transform.position.z - Target.transform.position.z)) / 100;
                float H = X + Z;
                float F = G + H;
                KeyValuePair<float, TileEdge> valuepair = new KeyValuePair<float, TileEdge>(F, item);
                if (Edge.To.Walkable && !TraveresedEdges.Contains(item) && !MinPriorityQueue.data.Contains(valuepair))
                {
                    MinPriorityQueue.Enqueue(valuepair);
                }
            }
        }
        TimeCalculated = DateTime.Now - StartTime;
        FunctionTime = (float)TimeCalculated.TotalMilliseconds;
        if (TargetNodeFound)
            GeneratedPath = CalculatePath(Source, Target);

        return TargetNodeFound;
    }
}