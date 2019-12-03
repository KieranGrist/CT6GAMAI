using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ASTAR :MonoBehaviour
{
    public static ASTAR aSTAR;
    public List<KeyValuePair<Node, Node>> NodesAlreadySearched = new List<KeyValuePair<Node, Node>>();
    public List<List<int>> RoutesGenerated = new List<List<int>>();
    private void Awake()
    {
        aSTAR = this;
    }
    private void Update()
    {
        aSTAR = this;
    }

    static float CalculateCost(Edge Edge, Node Target, List<float> Cost)
    {
        return (Cost[Edge.From.Index] + Edge.To.Cost) + ((Mathf.Abs(Edge.From.transform.position.x - Target.transform.position.x)) / 100 + (Mathf.Abs(Edge.From.transform.position.z - Target.transform.position.z)) / 100);
    }
    static List<int> CalculateRoute(Node Source, Node Target)
    {     
        bool TargetNodeFound = false;
        List<int> Route = new List<int>(NavGraph.map.Nodes.Count);
        List<float> Cost = new List<float>(NavGraph.map.Nodes.Count);
        for (int i = 0; i < NavGraph.map.Nodes.Count; i++)
        {
            Route.Add(-10);
            Cost.Add(int.MaxValue);
        }
        TargetNodeFound = false;
        HashSet<Edge> TraveresedEdges = new HashSet<Edge>();
        PriorityQueue<float, Edge> MinPriorityQueue = new PriorityQueue<float, Edge>();
        Cost[Source.Index] = 0;
        foreach (var item in Source.Neighbours)
        {
            KeyValuePair<float, Edge> keyValuePair = new KeyValuePair<float, Edge>(0, item);
            MinPriorityQueue.Enqueue(keyValuePair);
        }
        while (MinPriorityQueue.count > 0)
        {
            KeyValuePair<float, Edge> keyValuePair = MinPriorityQueue.Dequeue();
            TraveresedEdges.Add(keyValuePair.Value);
            Edge Edge = keyValuePair.Value;
            if (Cost[Edge.To.Index] > Cost[Edge.From.Index] + Edge.To.Cost)
            {
                Route[Edge.To.Index] = Edge.From.Index;
                Cost[Edge.To.Index] = CalculateCost(Edge, Target, Cost);
            }
            if (Edge.To.Index == Target.Index && !TargetNodeFound)
            {
                TargetNodeFound = true;
                break;
            }
            foreach (var item in Edge.To.Neighbours)
            {    
                float F = (item.To.Cost + Cost[Edge.To.Index] )+ ((Mathf.Abs(Edge.From.transform.position.x - Target.transform.position.x)) / 100 + (Mathf.Abs(Edge.From.transform.position.z - Target.transform.position.z)) / 100);
                var valuepair = new KeyValuePair<float, Edge>(F, item);
                if (!TraveresedEdges.Contains(item)) //Check if traveresed edges contains items before running the expensive contains for min priority queue
                if (Edge.To.Walkable  && !MinPriorityQueue.data.Contains(valuepair))
                    MinPriorityQueue.Enqueue(valuepair);
            }
        }

        return Route;
    }
    public List<int> CalculatePath(Node Source, Node Target)
    {
        var Route = new List<int>();
        if (!NodesAlreadySearched.Contains(new KeyValuePair<Node, Node>(Source, Target)))
            Route = CalculateRoute(Source, Target);
        else
        {
            var i =NodesAlreadySearched.IndexOf(new KeyValuePair<Node, Node>(Source, Target));
            return (RoutesGenerated[i]);
        }
        var GeneratedPath = new List<int>();
        int currentNode = Target.Index;
        GeneratedPath.Add(currentNode);
        while (currentNode != Source.Index)
        {
            currentNode = Route[currentNode];
            GeneratedPath.Add(currentNode);
        }
        NodesAlreadySearched.Add(new KeyValuePair<Node, Node>(Source, Target));
        RoutesGenerated.Add(GeneratedPath);
        return GeneratedPath;
    }
}