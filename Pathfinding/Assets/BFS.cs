﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BFS : Pathfinder
{
    public BFS(NavGraph navGraph) : base(navGraph)
    {

    }
    public Queue<GraphEdge> graphEdges = new Queue<GraphEdge>();
    public override bool CalculateRoute(GraphNode Source, GraphNode Target)
    {
        graphEdges = new Queue<GraphEdge>();
        Reset();
        for (int i = 0; i < Source.Neighbours.Count; i++)
        {
            graphEdges.Enqueue(Source.Neighbours[i]);
        }
        Visited[Source.Index] = true;
        while (graphEdges.Count > 0)
        {
            GraphEdge edge = graphEdges.Dequeue();
            Route[edge.To.Index] = edge.From.Index;
            Visited[edge.To.Index] = true;
            if (edge.To.Index == Target.Index)
            {
                GeneratedPath = CalculatePath(Source, Target);
                return true;
            }
            for (int i = 0; i < edge.To.Neighbours.Count; i++)
            {
                if (edge.To.Walkable && !Visited[edge.To.Neighbours[i].To.Index])
                {
                    graphEdges.Enqueue(edge.To.Neighbours[i]);
                }
            }

        }
        return false;
    }
}