using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class ASTAR 
{
    public TimeSpan TimeCalculated;
    public float FunctionTime;
    public bool TargetNodeFound = false;
    public List<int> Route = new List<int>();
    public List<float> Cost = new List<float>();
    public List<bool> Visited = new List<bool>();
    public List<int> GeneratedPath = new List<int>();

    public void TileReset()
    {
        Route = new List<int>(NavGraph.map.Nodes.Count);
        Visited = new List<bool>(NavGraph.map.Nodes.Count);
        Cost = new List<float>(NavGraph.map.Nodes.Count);
        for (int i = 0; i < NavGraph.map.Nodes.Count; i++)
        {
            Route.Add(-10);
            Cost.Add(int.MaxValue);
            Visited.Add(false);
        }
    }

    public List<int> CalculatePath(Node Source, Node Target)
    {
        List<int> Path = new List<int>();

        int currentNode = Target.Index;
        Path.Add(currentNode);
        while (currentNode != Source.Index)
        {
            currentNode = Route[currentNode];
            Path.Add(currentNode);
        }
        GeneratedPath = Path;
        return Path;
    }
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
    float CalculateCost(Edge Edge, Node Target)
    {
        float G = Cost[Edge.From.Index] + Edge.GetCost();
        float X = (Mathf.Abs(Edge.From.transform.position.x - Target.transform.position.x)) / 100;
        float Z = (Mathf.Abs(Edge.From.transform.position.z - Target.transform.position.z)) / 100;
        float H = X + Z;
        float F = G + H;
        return F;
    }
    public bool CalculateRoute(AIAgent ARTIE, Node Source, Node Target)
    {
        DateTime StartTime = DateTime.Now;
        TileReset();
        TargetNodeFound = false;
        HashSet<Edge> TraveresedEdges = new HashSet<Edge>();
        PriorityQueue<float, Edge> MinPriorityQueue = new PriorityQueue<float, Edge>();
        Cost[Source.Index] = 0;
        foreach (var item in Source.Neighbours)
        {
            KeyValuePair<float, Edge> keyValuePair = new KeyValuePair<float, Edge>(0, item);
            MinPriorityQueue.Enqueue(keyValuePair);
        }
        while (MinPriorityQueue.Count() > 0)
        {
            KeyValuePair<float, Edge> keyValuePair = MinPriorityQueue.Dequeue();
            TraveresedEdges.Add(keyValuePair.Value);
            Edge Edge = keyValuePair.Value;
            if (Cost[Edge.To.Index] > Cost[Edge.From.Index] + Edge.GetCost())
            {
                Route[Edge.To.Index] = Edge.From.Index;
                Cost[Edge.To.Index] = CalculateCost(Edge, Target);
            }
            if (Edge.To.Index == Target.Index && !TargetNodeFound)
            {
                TargetNodeFound = true;
                break;
            }
       
            foreach (var item in Edge.To.Neighbours)
            {
                float G = item.GetCost() + Cost[Edge.To.Index];
                float X = (Mathf.Abs(Edge.From.transform.position.x - Target.transform.position.x)) / 100;
                float Z = (Mathf.Abs(Edge.From.transform.position.z - Target.transform.position.z)) / 100;
                float H = X + Z;
                float F = G + H;
                KeyValuePair<float, Edge> valuepair = new KeyValuePair<float, Edge>(F, item);
                if (Edge.To.Walkable && !TraveresedEdges.Contains(item) && !MinPriorityQueue.data.Contains(valuepair))
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