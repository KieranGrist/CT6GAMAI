using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public  class ASTAR :MonoBehaviour
{
    public static ASTAR AStar;
    public List<KeyValuePair<Node, Node>> NodesAlreadySearched = new List<KeyValuePair<Node, Node>>();
    public List<List<int>> RoutesGenerated = new List<List<int>>();
    private void Awake()
    {
        AStar = this;
    }
    private void Update()
    {
        AStar = this;
    }
    static  List<int> CalculateRoute(Node Source, Node Target)
    {
        var Route = new List<int>(NavGraph.map.Nodes.Count);
        var Cost = new List<float>(NavGraph.map.Nodes.Count);


        for (int i = 0; i < NavGraph.map.Nodes.Count; i++)
        {
            Route.Add(-10);
            Cost.Add(int.MaxValue);
        }
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
            if (Cost[Edge.To.Index] > Cost[Edge.From.Index] + Edge.GetCost())
            {
                Route[Edge.To.Index] = Edge.From.Index;
                float HCost = (Mathf.Abs(Target.transform.position.x - Edge.To.transform.position.x)) + (Mathf.Abs(Target.transform.position.z - Edge.To.transform.position.z));
                float GCost = Cost[Edge.From.Index] + Edge.GetCost();
                float FCost = GCost + HCost;
                Cost[Edge.To.Index] = FCost;

            }

            if (Edge.To.Index == Target.Index)
                break;
            foreach (var item in Edge.To.Neighbours)
            {
                float HCost = (Mathf.Abs(Target.transform.position.x - Edge.To.transform.position.x)) + (Mathf.Abs(Target.transform.position.z - Edge.To.transform.position.z));
                float GCost = Cost[Edge.To.Index] + item.GetCost();
                float FCost = GCost + HCost;
                MinPriorityQueue.UpdateCost(item, FCost);
                KeyValuePair<float, Edge> valuepair = new KeyValuePair<float, Edge>(FCost, item);
                if (!TraveresedEdges.Contains(item))
                {

                    //Check if traveresed edges contains items before running the expensive contains for min priority queue
                    if (Edge.To.Walkable && !MinPriorityQueue.data.Contains(valuepair))
                    {
                        MinPriorityQueue.Enqueue(valuepair);

                    }
                }
            }
        }

        return Route;
    }

    public  List<int> CalculatePath(Node Source, Node Target)
    {
        var Route = new List<int>();
        if (!NodesAlreadySearched.Contains(new KeyValuePair<Node, Node>(Source, Target)))
            Route = CalculateRoute(Source, Target);
        else
        {
            var i = NodesAlreadySearched.IndexOf(new KeyValuePair<Node, Node>(Source, Target));
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