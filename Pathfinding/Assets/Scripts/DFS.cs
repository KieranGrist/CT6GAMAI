using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DFS : Pathfinder
{
    public Stack<GraphEdge> graphNodes = new Stack<GraphEdge>();
    // Start is called before the first frame update

    public override IEnumerator CalculateRoute(GraphNode Source, GraphNode Target)
    {
        CR_running = true;
        Route.Clear();
        Route = new List<GraphNode>();
        Visited.Clear();
        Visited = new List<bool>();
        graphNodes.Clear();
        graphNodes = new Stack<GraphEdge>();
        for (int i = 0; i < Graph.Nodes.Count; i++)
        {           
            Visited.Add(false);
        }
        Visited[Source.Index] = true;
        for (int i=0; i < Source.AdjacencyList.Count; i++)
        {
            graphNodes.Push(Source.AdjacencyList[i]);
        }
        while (graphNodes.Count > 0)
        {
           edge = graphNodes.Pop();
            edge.From.GetComponent<Renderer>().material.color = Color.black;
            if (!Route.Contains(edge.From))
            Route.Add(edge.From);
            Visited[edge.To.Index] = true;
            if (edge.To == Target)
            {
                Route.Add(edge.To);
                ReachedTarget = true;
                CR_running = false;
              StartCoroutine(  CalculatePath(Source,Target));
                yield break;
            }
            for (int i = 0; i < edge.To.AdjacencyList.Count; i++)
            {
                if (edge.To.Walkable)
                { 
                if (!Visited[edge.To.AdjacencyList[i].To.Index])
                {
                    graphNodes.Push(edge.To.AdjacencyList[i]);
                }
            }
            }
            yield return new WaitForSeconds(0.05f);
        }
        ReachedTarget = false;
        CR_running = false;
    }

}
