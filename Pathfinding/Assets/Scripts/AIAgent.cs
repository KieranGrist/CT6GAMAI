using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour {

    public string[] Names = new string[]
     {
    "Stainton",
   "Peters",
   "Stephenson",
    "Field",
   "Evans",
    "Mitchel",
    "Woods",
    "Grist",
    "Mitchell",
    "Newman",
    "Davey",
    "Rhodes",
    "Burke",
    "Howells",
    "Morgan",
    "Holland",
    "Flynn",
    "Watts",
    "Knight",
    "Bryant",
    "Leigh",
    "Gibson",
    "Gallagher",
    "Kelly",
    "Smith",
    "Holmes",
    "Bishop",
    "Dennis",
    "Hansen",
    "Spencer",
    "Baldwin",
    "Wilkinson",
    "Williams",
    "Griffiths",
    "Moss"
   };
    List<GameObject> PathGameObjects = new List<GameObject>();
   public NavGraph Map;
    float Distance;
    public bool RecievedPath = false;
    public bool Moving = false;
    public List<Vector3> TargetLocation = new List<Vector3>();
    public float InitialSpeed = 25.0f;
    public LayerMask Mask;
    public List<float> Speed = new List<float>();
    Vector3 Direction;
    Vector3 Normalise;
    Vector3 M;
    bool FinishedCalculation;
    bool ResetAllNodes;
    bool FoundRoute;
    public   TileNode SourceNode;
    public  TileNode TargetNode;
    void Start()
    {
        string ObjectName = name = "ARTIE " + Names[Random.Range(0, Names.Length)]; 
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mask))
            if (hit.transform.gameObject.GetComponent<TileNode>())
                SourceNode = hit.transform.gameObject.GetComponent<TileNode>();
        TargetNode = Map.Nodes[Random.Range(0, Map.Nodes.Count - 1)];
    }
    void MoveOnRoute()
    {
        if (FoundRoute && !RecievedPath)
        {
            foreach (var item in PathGameObjects)
                Destroy(item);

            TargetLocation.Clear();
            Speed.Clear();
            RecievedPath = true;
            for (int i = Map.PathfindingTechnique.GeneratedPath.Count - 1; i > 0; i--)
            {
                TargetLocation.Add(Map.Nodes[Map.PathfindingTechnique.GeneratedPath[i]].transform.position);
                Speed.Add(InitialSpeed / (float)Map.Nodes[Map.PathfindingTechnique.GeneratedPath[i]].Cost);
            }
            TargetLocation.Add(TargetNode.transform.position);
            Speed.Add(InitialSpeed);
        }

        if (TargetLocation.Count > 0)
        {
            if (TargetLocation[0] != null)
            {
                Distance = Vector3.Distance(TargetLocation[0], transform.position);

                if (Distance <= 2)
                {
                    transform.position = TargetLocation[0];
                    Moving = false;
                    TargetLocation.Remove(TargetLocation[0]);
                    Speed.Remove(Speed[0]);
                }
                else
                {
                    Moving = true;
                }

            }

            if (Moving == true)
            {
                TargetLocation[0] = new Vector3(TargetLocation[0].x, 10, TargetLocation[0].z);
                Direction = TargetLocation[0] - transform.position;
                Normalise = Direction.normalized;
                M = Normalise * Time.deltaTime * Speed[0];
                transform.position += M;
            }
        }
        else
        {
            if (!FinishedCalculation)
            {
                TargetNode = Map.Nodes[Random.Range(0, Map.Nodes.Count - 1)];
                FoundRoute = false;
                RecievedPath = false;
            }
        }
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mask))
            if (hit.transform.gameObject.GetComponent<TileNode>())
                SourceNode = hit.transform.gameObject.GetComponent<TileNode>();

        MoveOnRoute();

        if (!FoundRoute && SourceNode != null)
            FoundRoute = Map.PathfindingTechnique.CalculateRoute(SourceNode, TargetNode);
    }
}
