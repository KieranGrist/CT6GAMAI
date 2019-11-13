using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]


public abstract class Pathfinder : MonoBehaviour
{
    public TimeSpan TimeCalculated;
    public float FunctionTime;
    public bool TargetNodeFound = false;
    public List<int> Route = new List<int>();
    public List<float> Cost = new List<float>();
    public List<bool> Visited = new List<bool>();
    public List<int> GeneratedPath = new List<int>();


    public abstract bool CalculateRoute(TileNode Source, TileNode Target);
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

    public List<int> CalculatePath(TileNode Source, TileNode Target) 
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
}
