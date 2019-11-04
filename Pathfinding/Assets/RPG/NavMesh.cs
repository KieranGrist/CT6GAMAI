using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMesh : MonoBehaviour
{
    public Pathfinder PathfindingTechnique;
    Pathfinder PreviousPathfinder;
    public List<TileNode> Nodes = new List<TileNode>();
    TileNode PreviousSource;
    TileNode PreviousTarget;
    public TileNode SourceNode;
    public TileNode TargetNode;
    public bool FoundRoute;
    bool RouteDrawn;
    bool PathDrawn;
    int PreviousArea = int.MinValue;
    public bool GenerateGraph = true;
    // Start is called before the first frame update
    void Start()
    {
        PreviousPathfinder = PathfindingTechnique;
        Nodes.AddRange(FindObjectsOfType<TileNode>());
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].name = "Node " + i;
            Nodes[i].Index = i;
            Nodes[i].Reset();
        }
        SourceNode = Nodes[0];
        SourceNode.GetComponent<Material>().color = Color.green;
        TargetNode = Nodes[4];
        TargetNode.GetComponent<Material>().color = Color.red;
        PreviousSource = SourceNode;
        PreviousTarget = TargetNode;

    }
    void DrawRoute()
    {
        RouteDrawn = true;
        for (int i = 0; i < PathfindingTechnique.Route.Count; i++)
        {
            if (PathfindingTechnique.Route[i] != -10)
                Nodes[PathfindingTechnique.Route[i]].GetComponent<Material>().color = Color.black;

        }

    }
    void DrawPath()
    {
        PathDrawn = true;
        for (int i = PathfindingTechnique.GeneratedPath.Count - 1; i > 0; i--)
        {
            Nodes[PathfindingTechnique.GeneratedPath[i]].GetComponent<Material>().color = Color.yellow;

        }

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            PathfindingTechnique.Route.Clear();
            PathfindingTechnique.GeneratedPath.Clear();
            FoundRoute = false;
            RouteDrawn = false;
            PathDrawn = false;
            StopAllCoroutines();
        }
        if (PreviousSource != SourceNode || PreviousTarget != TargetNode || PathfindingTechnique != PreviousPathfinder)
        {
            PathfindingTechnique.Route.Clear();
            PathfindingTechnique.GeneratedPath.Clear();

            FoundRoute = false;
            RouteDrawn = false;
            PathDrawn = false;
            PreviousSource = SourceNode;
            PreviousTarget = TargetNode;
            PreviousPathfinder = PathfindingTechnique;
        }

    }

}
