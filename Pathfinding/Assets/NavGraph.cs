using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NavGraph : MonoBehaviour
{
    public Pathfinder PathfindingTechnique;
    public List<GraphNode> Nodes;
    public GraphNode Source, Target;
    public bool FoundRoute;
    void Start()
    {
    
    }
    private void OnDrawGizmosSelected()
    {
        //foreach (var node in Nodes)
        //{
        //    foreach (var item in node.Neighbours)
        //    {
        //        Gizmos.color = Color.blue;
        //        Gizmos.DrawLine( item.From.transform.position, item.To.transform.position);
        //    }
        //}
    }
    void Update()
    {
        for (int i =0; i < Nodes.Count; i++)
        {
            Nodes[i].Index = i;
        }
        if (!FoundRoute)        
            FoundRoute = PathfindingTechnique.CalculateRoute(Source, Target);
    }
}