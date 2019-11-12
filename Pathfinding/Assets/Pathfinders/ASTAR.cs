using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ASTAR : Pathfinder
{
    public float F, G, H;
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

    public override bool CalculateRoute(TileNode Source, TileNode Target)
    {
        DateTime StartTime = DateTime.Now;
        TileReset();
        TargetNodeFound = false;
        List<TileEdge> TraveresedEdges = new List<TileEdge>();
        PriorityQueue<float, TileEdge> MinPriorityQueue = new PriorityQueue<float, TileEdge>();

        Cost[Source.Index] = 0;
        foreach(var item in Source.Neighbours)
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
              
                G = Cost[Edge.From.Index] + Edge.GetCost();
                float X = Mathf.Abs(Edge.From.transform.position.x - Target.transform.position.x);
                float Y = Mathf.Abs(Edge.From.transform.position.z - Target.transform.position.z);
                H =  X + Y;
                H /= 100;
                F = G + H;
                Route[Edge.To.Index] = Edge.From.Index;
                Cost[Edge.To.Index] = F;
            }
            if (Edge.To.Index == Target.Index)
            {
                TargetNodeFound = true;
            }
            foreach (var item in Edge.To.Neighbours)
            {
                G = Cost[Edge.From.Index] + Edge.GetCost();
                H = Mathf.Abs(Edge.From.transform.position.x - Target.transform.position.x) + Mathf.Abs(Edge.From.transform.position.z - Target.transform.position.z);
                H /= 100;
                F = G + H;
                KeyValuePair<float, TileEdge> valuepair = new KeyValuePair<float, TileEdge>(F, item);
                if (Edge.To.Walkable && !TraveresedEdges.Contains(item) && !MinPriorityQueue.data.Contains(valuepair))
                {
                    MinPriorityQueue.Enqueue(valuepair);
                }
            }
        }
        TimeCalculated =  DateTime.Now - StartTime;
        FunctionTime = (float)TimeCalculated.TotalMilliseconds;
        if (TargetNodeFound)
            GeneratedPath = CalculatePath(Source, Target);

        return TargetNodeFound;
    }
}