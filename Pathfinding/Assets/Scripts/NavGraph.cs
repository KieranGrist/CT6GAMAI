using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class NavGraph : MonoBehaviour
{    
    public Pathfinder pathfindingTechnique;
    Pathfinder PreviousPathfinder;
    public GameObject GO_Node;
    public GraphNode SourceNode, TargetNode;
    GraphNode PreviousSource, PreviousTarget;
    public List<GraphNode> Nodes = new List<GraphNode>();
    public int Area = 15;
    int PreviousArea = int.MinValue;
    void Start()
    {
        PreviousPathfinder = pathfindingTechnique;
        pathfindingTechnique.Graph = GetComponent<NavGraph>();
    }
    public void CalculateNewGraph()
    {
        for (float x = transform.position.x - Area; x < transform.position.x + Area; x += 2)
        {
            for (float z = transform.position.z - Area; z < transform.position.z + Area; z += 2)
            {
                GameObject go = Instantiate(GO_Node, transform.position, transform.rotation);
                go.transform.position = new Vector3(x, 2, z);
                Nodes.Add(go.GetComponent<GraphNode>());
            }
        }
        SourceNode = Nodes[0];
        SourceNode.GetComponent<Renderer>().material.color = Color.green;
        TargetNode = Nodes[1];
        TargetNode.GetComponent<Renderer>().material.color = Color.red;
        PreviousSource = SourceNode;
        PreviousTarget = TargetNode;
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].Reset();
            Nodes[i].name = "Node " + i;
            Nodes[i].Index = i;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pathfindingTechnique.ReachedTarget = true;
        }
        if (Area != PreviousArea)
        {
            CalculateNewGraph();
        }
        PreviousArea = Area;
        SourceNode.GetComponent<Renderer>().material.color = Color.green;
        TargetNode.GetComponent<Renderer>().material.color = Color.red;
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i] != SourceNode && Nodes[i] != TargetNode && !pathfindingTechnique.Route.Contains(Nodes[i]) && pathfindingTechnique.Path.Contains(Nodes[i]))
            {
                Nodes[i].GetComponent<Renderer>().material.color = Color.yellow;

            }
            if (Nodes[i] != SourceNode && Nodes[i] != TargetNode && pathfindingTechnique.Route.Contains(Nodes[i]) && !pathfindingTechnique.Path.Contains(Nodes[i]))
                {
                Nodes[i].GetComponent<Renderer>().material.color = Color.black;
            }
            if (Nodes[i] != SourceNode && Nodes[i] != TargetNode && Nodes[i].Walkable && !pathfindingTechnique.Route.Contains(Nodes[i]) && !pathfindingTechnique.Path.Contains(Nodes[i]))
            {
                Nodes[i].GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        if (!pathfindingTechnique.CR_running)
        {
            if (!pathfindingTechnique.ReachedTarget)
            {
                StartCoroutine(pathfindingTechnique.CalculateRoute(SourceNode, TargetNode));
            }
        }
        if (pathfindingTechnique.ReachedTarget)
        {
            if (Input.GetMouseButtonDown(2))
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    hit.transform.gameObject.GetComponent<GraphNode>().Walkable = !hit.transform.gameObject.GetComponent<GraphNode>().Walkable;
                }
            }
                if (Input.GetMouseButtonDown(0))
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    PreviousSource.GetComponent<Renderer>().material.color = Color.blue;
                    SourceNode = hit.transform.gameObject.GetComponent<GraphNode>();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    PreviousTarget.GetComponent<Renderer>().material.color = Color.blue;
                    TargetNode = hit.transform.gameObject.GetComponent<GraphNode>();
                }
            }
            if (PreviousSource != SourceNode|| PreviousTarget != TargetNode || pathfindingTechnique != PreviousPathfinder)
            {
                pathfindingTechnique.Route.Clear();
                pathfindingTechnique.Path.Clear();
                pathfindingTechnique.ReachedTarget = false;
            }
            for (int i = 0; i < pathfindingTechnique.Path.Count - 1; i++)
            {
                Debug.DrawLine(pathfindingTechnique.Path[i].transform.position, pathfindingTechnique.Path[i + 1].transform.position, Color.red);
            }
            PreviousSource = SourceNode;
            PreviousTarget = TargetNode;
            PreviousPathfinder = pathfindingTechnique;
        }
      
    }
}
