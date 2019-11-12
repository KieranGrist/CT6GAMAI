using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class BFS : Pathfinder
{
    public override bool CalculateRoute(TileNode Source, TileNode Target)
    {
        DateTime StartTime = DateTime.Now;
        Queue<TileEdge> graphEdges = new Queue<TileEdge>();
        graphEdges = new Queue<TileEdge>();
        TileReset();
        for (int i = 0; i < Source.Neighbours.Count; i++)
        {
            graphEdges.Enqueue(Source.Neighbours[i]);
        }
        Visited[Source.Index] = true;
        while (graphEdges.Count > 0)
        {
            TileEdge edge = graphEdges.Dequeue();
            Route[edge.To.Index] = edge.From.Index;
            Visited[edge.To.Index] = true;
            if (edge.To.Index == Target.Index)
            {
                TimeCalculated = DateTime.Now - StartTime;
                FunctionTime = (float)TimeCalculated.TotalMilliseconds;
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
        TimeCalculated = DateTime.Now - StartTime;
        FunctionTime = (float)TimeCalculated.TotalMilliseconds;
        return false;
    }
}
