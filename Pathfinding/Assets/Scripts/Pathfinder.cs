using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Pathfinder : MonoBehaviour
{
    public List<bool> Visited = new List<bool>();
    public GraphEdge edge;
    public List<GraphNode> Route = new List<GraphNode>();
    public NavGraph Graph;
    public bool CR_running = false;
    public bool ReachedTarget;
    public abstract IEnumerator CalculateRoute(GraphNode Source, GraphNode Target);
}
