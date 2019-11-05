using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NavGraph : MonoBehaviour
{
    public float PathCost;
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
    int PreviousArea = int.MinValue;
    public bool GenerateGraph = true;
    // Start is called before the first frame update
    void Start()
    {
        PreviousPathfinder = PathfindingTechnique;
        for (int i = 0; i < Nodes.Count; i++)
        {
            // Nodes[i].name = "Tile " + i;

            Nodes[i].Index = i;
            Nodes[i].MaterialManager = MaterialManager;
            Nodes[i].GetComponent<TileNode>().enabled = true;
            Nodes[i].Reset();
        }
        SourceNode = Nodes[0];
        TargetNode = Nodes[4];
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
                GO.transform.position += new Vector3(0, 1, 0);
                GO.name = "Route " + x;x++;
                RouteGameObjects.Add(GO);
            }
        }

    }
    void DrawPath()
    {
        PathDrawn = true;
        PathCost = 0;
 
        for (int i = 0; i < PathGameObjects.Count; i++)
            Destroy(PathGameObjects[i]);
        PathGameObjects.Clear();
        int x = 0;
        for (int i = 0; i < PathfindingTechnique.GeneratedPath.Count; i++)
        {
           
                GameObject GO = Instantiate(Cube, transform.position, transform.rotation);
                GO.transform.position = Nodes[PathfindingTechnique.GeneratedPath[i]].transform.position;
                PathCost += Nodes[PathfindingTechnique.GeneratedPath[i]].Cost;
                GO.GetComponent<Renderer>().material.color = new Color(0, 0, 255, 0.1f);
                GO.transform.position += new Vector3(0, 1, 0);
                GO.name = "Path " + x; x++;
                PathGameObjects.Add(GO);
            
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


        if (!FoundRoute)
            FoundRoute = PathfindingTechnique.CalculateRoute(SourceNode, TargetNode);
        else
        {

            if (!RouteDrawn)
                DrawRoute();
            if (!PathDrawn)
                DrawPath();

        }
    }
}
