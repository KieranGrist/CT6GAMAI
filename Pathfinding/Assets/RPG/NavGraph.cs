using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class NavGraph : MonoBehaviour
{
    public TileMaterials MaterialManager;
    public Pathfinder PathfindingTechnique;
    Pathfinder PreviousPathfinder;
    public GameObject Cube;
    public List<TileNode> Nodes = new List<TileNode>();
    public List<GameObject> RouteGameObjects = new List<GameObject>();
    public List<GameObject> PathGameObjects = new List<GameObject>();
    TileNode PreviousSource;
    TileNode PreviousTarget;
    public TileNode SourceNode;
    public TileNode TargetNode;
    public bool FoundRoute;
    bool RouteDrawn;
    bool PathDrawn;

    // Start is called before the first frame update
  
    void Start()
    {
        PreviousPathfinder = PathfindingTechnique;
        Nodes.Clear();
        Nodes.AddRange(FindObjectsOfType<TileNode>());
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].Index = i;
            Nodes[i].MaterialManager = MaterialManager;
            Nodes[i].GetComponent<TileNode>().enabled = true;
            Nodes[i].Reset();
        }
        SourceNode = Nodes[0];
        TargetNode = Nodes[Nodes.Count - 1];
        PreviousSource = SourceNode;
        PreviousTarget = TargetNode;

    }
    void DrawRoute()
    {
        RouteDrawn = true;
     
        for (int i = 0; i < RouteGameObjects.Count; i++)
            Destroy(RouteGameObjects[i]);
        RouteGameObjects.Clear();
        int x = 0;
        for (int i = 0; i < PathfindingTechnique.Route.Count; i++)
        {
            if (!PathfindingTechnique.GeneratedPath.Contains(PathfindingTechnique.Route[i]))
            if (PathfindingTechnique.Route[i] != -10)
            {
                GameObject GO = Instantiate(Cube,transform.position , transform.rotation);
                GO.transform.position = Nodes[PathfindingTechnique.Route[i]].transform.position;
                GO.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.5f);
                GO.transform.position += new Vector3(0, 5, 0);
                GO.name = "Route " + x;x++;
                RouteGameObjects.Add(GO);
            }
        }

    }
    void DrawPath()
    {
        PathDrawn = true;
     
 
        for (int i = 0; i < PathGameObjects.Count; i++)
            Destroy(PathGameObjects[i]);
        PathGameObjects.Clear();
        int x = 0;
        for (int i = 0; i < PathfindingTechnique.GeneratedPath.Count; i++)
        {
            if (Nodes[PathfindingTechnique.GeneratedPath[i]] != SourceNode && Nodes[PathfindingTechnique.GeneratedPath[i]] != TargetNode)
            {
                GameObject GO = Instantiate(Cube, transform.position, transform.rotation);
                GO.transform.position = Nodes[PathfindingTechnique.GeneratedPath[i]].transform.position;
             
                GO.GetComponent<Renderer>().material.color = new Color(0, 0, 255, 0.1f);
                GO.transform.position += new Vector3(0, 5, 0);
                GO.name = "Path " + x; x++;
                PathGameObjects.Add(GO);
            }
            else if (Nodes[PathfindingTechnique.GeneratedPath[i]] == SourceNode)
            {
                GameObject GO = Instantiate(Cube, transform.position, transform.rotation);
                GO.transform.position = Nodes[PathfindingTechnique.GeneratedPath[i]].transform.position;             
                GO.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
                GO.transform.position += new Vector3(0, 5, 0);
                GO.name = "Source Node" ; x++;
                PathGameObjects.Add(GO);
            }
            else if (Nodes[PathfindingTechnique.GeneratedPath[i]] == TargetNode)
            {
                GameObject GO = Instantiate(Cube, transform.position, transform.rotation);
                GO.transform.position = Nodes[PathfindingTechnique.GeneratedPath[i]].transform.position;                 
                GO.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 127);
                GO.transform.position += new Vector3(0, 5, 0);
                GO.name = "Target Node"; x++;
                PathGameObjects.Add(GO);
            }
        }

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
                SourceNode = hit.transform.gameObject.GetComponent<TileNode>();
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
                TargetNode = hit.transform.gameObject.GetComponent<TileNode>();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        GraphInteraction();
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


       // if (!FoundRoute)
       //     FoundRoute = PathfindingTechnique.CalculateRoute(SourceNode, TargetNode);
        else
        {

            if (!RouteDrawn)
                DrawRoute();
            if (!PathDrawn)
                DrawPath();

        }
    }
}
