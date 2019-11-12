using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum PathfinderType
{
    BreadthFirstSeach,
    DepthFirstSearch,
    Dijkstras,
    AStar
}
[System.Serializable]
[ExecuteInEditMode]
public class NavGraph : MonoBehaviour
{
    public AIAgent ARTIE;
    public TileMaterials MaterialManager;
    public PathfinderType pathfindingType;
    public Pathfinder PathfindingTechnique;
    public TileNode SourceNode;
    public TileNode TargetNode;
    public LayerMask TileMask;
    public GameObject Cube;
    public List<TileNode> Nodes = new List<TileNode>();
    public bool ResetAllNodes;
    public bool FoundRoute;
    Pathfinder PreviousTechnique;
    PathfinderType PreviousPathfinder;
    GameObject TargetGameObject;
    TileNode PreviousSource;
    TileNode PreviousTarget;
    void TechniqueSelector()
    {
        if (pathfindingType != PreviousPathfinder)
        {
            foreach (var item in gameObject.GetComponents<Pathfinder>())
               DestroyImmediate(item); 

            switch (pathfindingType)
            {
                case PathfinderType.AStar:
                    gameObject.AddComponent<ASTAR>();
                    break;
                case PathfinderType.BreadthFirstSeach:
                    gameObject.AddComponent<BFS>();
                    break;
                case PathfinderType.DepthFirstSearch:
                    gameObject.AddComponent<DFS>();
                    break;
                case PathfinderType.Dijkstras:
                    gameObject.AddComponent<Dijkstras>();
                    break;
            }
            PathfindingTechnique = gameObject.GetComponent<Pathfinder>();
            gameObject.GetComponent<Pathfinder>().TileMap = GetComponent<NavGraph>();
            PreviousPathfinder = pathfindingType;
        }

    }
    void AddNodes()
    {
        Nodes.Clear();
        List<TileNode> TempNodesList = new List<TileNode>();
        TempNodesList.AddRange(FindObjectsOfType<TileNode>());
        foreach(var item in TempNodesList)      
                Nodes.Add(item);     
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i] == null)
                Nodes.Remove(Nodes[i]);
            else
            {

                Nodes[i].Index = i;
                Nodes[i].MaterialManager = MaterialManager;
                Nodes[i].GetComponent<TileNode>().enabled = true;
                Nodes[i].Reset();
            }
        }
    }
    void Start()
    {
        TechniqueSelector();
        InvokeRepeating("AddNodes", 0, 10);
        SourceNode = Nodes[0];
        TargetNode = Nodes[Nodes.Count - 1];
        PreviousSource = SourceNode;
        PreviousTarget = TargetNode;        
    }

    void DrawFinish()
    {
        foreach (var item in Nodes)
            if (item == TargetNode)
            {
                Destroy(TargetGameObject);
                TargetGameObject = Instantiate(Cube, transform.position, transform.rotation);
                TargetGameObject.transform.position = item.transform.position;
                TargetGameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 127);
                TargetGameObject.transform.position += new Vector3(0, 1, 0);
                TargetGameObject.transform.localScale = new Vector3(100, 100, 0.05f);
                TargetGameObject.name = "Target Node";
            }
    }
    void GraphInteraction()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, TileMask))
            {
                FoundRoute = false;
                TargetNode = hit.transform.gameObject.GetComponent<TileNode>();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
   
        TechniqueSelector();
  
        if (ResetAllNodes)
        {
            AddNodes();
            foreach (var item in FindObjectsOfType<TileNode>())
                item.Reset();

            ResetAllNodes = false;
        }

        if (Application.isPlaying)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                PathfindingTechnique.Route.Clear();
                PathfindingTechnique.GeneratedPath.Clear();
                FoundRoute = false;
            }
            GraphInteraction();
            if (PreviousTarget != TargetNode || PathfindingTechnique != PreviousTechnique)
            {
                PathfindingTechnique.Route.Clear();
                PathfindingTechnique.GeneratedPath.Clear();
                FoundRoute = false;
                PreviousSource = SourceNode;
                PreviousTarget = TargetNode;
                PreviousTechnique = PathfindingTechnique;
            }
            if (!FoundRoute)
                FoundRoute = PathfindingTechnique.CalculateRoute(SourceNode, TargetNode);
            DrawFinish();
        }
    }
}
