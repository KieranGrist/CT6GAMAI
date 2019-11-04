using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GraphMap : MonoBehaviour
{
    public Pathfinder PathfindingTechnique;
    Pathfinder PreviousPathfinder;
    public GameObject GO_Node;
    public List<GraphNode> Nodes = new List<GraphNode>();
    GraphNode PreviousSource;
    GraphNode PreviousTarget;
    public GraphNode SourceNode;
    public GraphNode TargetNode;
    public bool FoundRoute;
    public int Area = 15;
    bool RouteDrawn;
    bool PathDrawn;
    int PreviousArea = int.MinValue;
    public bool GenerateGraph = true;
    void Start()
    {     
        PreviousPathfinder = PathfindingTechnique;
        if (GenerateGraph)
            CalculateNewGraph();
        else
        {
            Nodes.AddRange( FindObjectsOfType<GraphNode>());
            PreviousArea = Area;    
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].name = "Node " + i;
                Nodes[i].Index = i;
             //   Nodes[i].Reset();
            }
            SourceNode = Nodes[0];
            SourceNode.GetComponent<Material>().color = Color.green;
            TargetNode = Nodes[4];
            TargetNode.GetComponent<Material>().color = Color.red;
            PreviousSource = SourceNode;
            PreviousTarget = TargetNode;
        }
    }

    public void CalculateNewGraph()
    {
        Nodes = new List<GraphNode>();
        for (float x = transform.position.x - Area; x < transform.position.x + Area; x += 2)
        {
            for (float z = transform.position.z - Area; z < transform.position.z + Area; z += 2)
            {
                GameObject go = Instantiate(GO_Node, transform.position, transform.rotation);
                go.transform.position = new Vector3(x, 2, z);
                go.transform.parent = GetComponent<Transform>();
                go.transform.position = new Vector3(x, 2, z);
                Nodes.Add(go.GetComponent<GraphNode>());
            }
        }
        SourceNode = Nodes[0];
        SourceNode.GetComponent<Material>().color = Color.green;
        TargetNode = Nodes[4];
        TargetNode.GetComponent<Material>().color = Color.red;
        PreviousSource = SourceNode;
        PreviousTarget = TargetNode;
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].Reset();
            Nodes[i].name = "Node " + i;
            Nodes[i].Index = i;           
        }
        PreviousArea = Area;
    }
    void GraphInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                FoundRoute = false;
                RouteDrawn = false;
                PathDrawn = false;
                PreviousSource.GetComponent<Material>().color = Color.blue;
                SourceNode = hit.transform.gameObject.GetComponent<GraphNode>();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                FoundRoute = false;
                RouteDrawn = false;
                PathDrawn = false;
                PreviousTarget.GetComponent<Material>().color = Color.blue;
                TargetNode = hit.transform.gameObject.GetComponent<GraphNode>();
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                hit.transform.gameObject.GetComponent<GraphNode>().Walkable = !hit.transform.gameObject.GetComponent<GraphNode>().Walkable;
            }
        }
        SourceNode.GetComponent<Material>().color = Color.green;
        TargetNode.GetComponent<Material>().color = Color.red;
    }
    void DrawRoute()
    {
        RouteDrawn = true;     
        for (int i = 0; i < PathfindingTechnique.Route.Count; i++)
        {
            if (PathfindingTechnique.Route[i]!=-10)
            Nodes[PathfindingTechnique.Route[i]].GetComponent<Material>().color = Color.black;

        }

    }
    void DrawPath()
    {
        PathDrawn = true;
        for (int i = PathfindingTechnique.GeneratedPath.Count -1; i > 0; i--)
        {
            Nodes[PathfindingTechnique.GeneratedPath[i]].GetComponent<Material>().color = Color.yellow;

        }

    }
    void Update()
    {
        GraphInteraction();
        if (Area != PreviousArea)
        {
            CalculateNewGraph();
        }
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
            for (int i = 0; i < Nodes.Count; i++)
                Nodes[i].GetComponent<Material>().color = Color.blue;

            FoundRoute = false;
            RouteDrawn = false;
            PathDrawn = false;
            PreviousSource = SourceNode;
            PreviousTarget = TargetNode;
            PreviousPathfinder = PathfindingTechnique;
        }
     
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].Index = i;
        }
        if (!FoundRoute)
            FoundRoute = PathfindingTechnique.CalculateRoute(SourceNode, TargetNode);
        else
        {
   
            if (!RouteDrawn)
                DrawRoute();
            if (!PathDrawn)
                DrawPath();
   
        }
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (!PathfindingTechnique.Route.Contains(Nodes[i].Index) && !PathfindingTechnique.GeneratedPath.Contains(Nodes[i].Index) && Nodes[i] != SourceNode && Nodes[i] != TargetNode && Nodes[i].Walkable)

                Nodes[i].GetComponent<Material>().color = Color.blue;

            if (!Nodes[i].Walkable)
                Nodes[i].GetComponent<Material>().color = Color.white;

        }
        PreviousArea = Area;
    }
}