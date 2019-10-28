using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Pathfinder : MonoBehaviour
{
    public List<GraphNode> Path = new List<GraphNode>();
    public List<bool> Visited = new List<bool>();
    public GraphEdge edge;
    public List<GraphNode> Route = new List<GraphNode>();
    public NavGraph Graph;
    public bool CR_running = false;
    public bool ReachedTarget;
    public abstract IEnumerator CalculateRoute(GraphNode Source, GraphNode Target);
    public IEnumerator CalculatePath(GraphNode Source, GraphNode Target)
    {
        GraphNode PreviousRouteNode = new GraphNode();
        for (int i = 0; i < Route.Count; i++)
        {
            Route[i].GetComponent<Renderer>().material.color = Color.yellow;


            Path.Add(Route[i]);
            yield return new WaitForSeconds(0.05f);
        }
    }
}