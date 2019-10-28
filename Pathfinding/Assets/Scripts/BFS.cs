using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : Pathfinder
{
    public Queue<GraphEdge> graphNodes = new Queue<GraphEdge>();
    public override IEnumerator CalculateRoute(GraphNode Source, GraphNode Target)
    {
        CR_running = true;
        Route.Clear();
        Route = new List<GraphNode>();
        Visited.Clear();
        Visited = new List<bool>();
        graphNodes.Clear();
        graphNodes = new Queue<GraphEdge>();
        for (int i = 0; i < Graph.Nodes.Count; i++)
        {
            Visited.Add(false);
        }
        Visited[Source.Index] = true;
        for (int i = 0; i < Source.AdjacencyList.Count; i++)
        {
            graphNodes.Enqueue(Source.AdjacencyList[i]);
        }
        while (graphNodes.Count > 0)
        {
            edge = graphNodes.Dequeue();
            edge.From.GetComponent<Renderer>().material.color = Color.black;
            if (!Route.Contains(edge.From))
                Route.Add(edge.From);
            Visited[edge.To.Index] = true;
            if (edge.To == Target)
            {
                Route.Add(edge.To);
                ReachedTarget = true;
                CR_running = false;
                StartCoroutine(CalculatePath(Source, Target));
                yield break;
            }
            for (int i = 0; i < edge.To.AdjacencyList.Count; i++)
            {
                if (edge.To.Walkable)
                {
                    if (!Visited[edge.To.AdjacencyList[i].To.Index])
                    {
                        graphNodes.Enqueue(edge.To.AdjacencyList[i]);
                    }
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
        ReachedTarget = false;
        CR_running = false;
    }

}
