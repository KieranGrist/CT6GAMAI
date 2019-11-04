using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Pathfinder : MonoBehaviour
{
    public NavMesh TileMap;
    public GraphMap Graph;
    public List<int> Route = new List<int>();
    public List<bool> Visited = new List<bool>();
    public List<int> GeneratedPath = new List<int>();
    public Pathfinder(GraphMap Graph)
    {
        this.Graph = Graph;
    }
    public abstract bool CalculateRoute(GraphNode Source, GraphNode Target);
    public void Reset()
    {
        Route = new List<int>(Graph.Nodes.Count);       
        Visited = new List<bool>(Graph.Nodes.Count);
        for (int i = 0; i < Graph.Nodes.Count; i++) 
        {
            Route.Add(-10);
            Visited.Add(false);
        }
    }
    public List<int> CalculatePath(GraphNode Source, GraphNode Target)
    {
        List<int> Path = new List<int>();

        int currentNode = Target.Index;
        Path.Add(currentNode);
        while (currentNode != Source.Index)
        {
            currentNode = Route[currentNode];
            Path.Add(currentNode);
        }        
        return Path;
    }
}
