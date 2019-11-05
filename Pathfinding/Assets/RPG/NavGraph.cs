using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class NavGraph : MonoBehaviour
{
    public AIAgent ARTIE;
    public TileMaterials MaterialManager;
    public Pathfinder PathfindingTechnique;
    Pathfinder PreviousPathfinder;
    public GameObject Cube;
    public List<TileNode> Nodes = new List<TileNode>();
    GameObject TargetGameObject;
    TileNode PreviousSource;
    TileNode PreviousTarget;
    public TileNode SourceNode;
    public TileNode TargetNode;
    bool SourceDrawn, TargetDrawn;
    public bool FoundRoute;
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
        PreviousSource = SourceNode;
        PreviousTarget = TargetNode;

    }

    void DrawFinish()
    {  
        foreach (var item in Nodes)
        {
            if (item == TargetNode)
            {
                Destroy(TargetGameObject);
                TargetGameObject = Instantiate(Cube, transform.position, transform.rotation);
                TargetGameObject.transform.position = item.transform.position;
                TargetGameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 127);
                TargetGameObject.transform.position += new Vector3(0, 1, 0);
                TargetGameObject.transform.localScale = new Vector3(1, 1, 0.05f);
                TargetGameObject.name = "Target Node";
            }
        }

    }
    void GraphInteraction()
    {       
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                FoundRoute = false;               
                TargetNode = hit.transform.gameObject.GetComponent<TileNode>();
                ARTIE.RecievedPath = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {       
        if (!Application.isPlaying)
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
            TargetNode = Nodes[200];
            PreviousSource = SourceNode;
            PreviousTarget = TargetNode;

        }
        else
        {
            GraphInteraction();
            if (Input.GetKey(KeyCode.Space))
            {
                PathfindingTechnique.Route.Clear();
                PathfindingTechnique.GeneratedPath.Clear();
                FoundRoute = false;      
                StopAllCoroutines();
            }
            if (PreviousTarget != TargetNode || PathfindingTechnique != PreviousPathfinder)
            {
                SourceDrawn = false;
                TargetDrawn = false;     
                PathfindingTechnique.Route.Clear();
                PathfindingTechnique.GeneratedPath.Clear();
                FoundRoute = false;      
                PreviousSource = SourceNode;
                PreviousTarget = TargetNode;
                PreviousPathfinder = PathfindingTechnique;
            }
            if (!FoundRoute)            
                FoundRoute = PathfindingTechnique.CalculateRoute(SourceNode, TargetNode);
   
         DrawFinish();
        }
    }
}
