using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIAgent : MonoBehaviour
{
    string[] FirstNames = new string[]
    {
        "Kieran",
        "Alex",
        "Gavin",
        "Lawrence",
       "Alice",
        "Sophie",
        "Iona",
        "Chloe",
        "Lucia",
        "Dale",
        "Georgina",
        "Nicole",
        "Keria",
       "Haleema",
       "Helen",
       "Emily",
       "Liberty",
       "Faye",
       "Carrie",
       "Elsie",
       "Crystal",
       "Maria",
       "Ayla",
       "Aminah",
       "Amie",
       "Jack",
       "Ben",
       "Adam",
       "Tegan",
       "Edan",
       "Aliceson",
       "Merle",
       "Aiden",
       "Allyson",
       "Lyndsey",
       "Stacia",       
        "Lauren"
    };
    string[] LastNames = new string[]
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
    "Brown",
    "Shade",
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
    "Wade",
    "Williams",
    "Griffiths",
    "Moss"
   };
    public bool Military;
    public bool Friendly = false;
    List<GameObject> PathGameObjects = new List<GameObject>();
    public bool RecievedPath = false;
    bool Moving = false;
    List<Vector3> TargetLocation = new List<Vector3>();
    float InitialSpeed = 250.0f;
    public LayerMask Mask;
    List<float> Speed = new List<float>();
    bool ResetAllNodes;
    bool FoundRoute;
    public TileNode SourceNode;
    public TileNode TargetNode;
    void Start()
    {
        Friendly = Random.value >= 0.5;
        string ObjectName = name = FirstNames[Random.Range(0, FirstNames.Length)] + " " + LastNames[Random.Range(0, LastNames.Length)];
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mask))
            if (hit.transform.gameObject.GetComponent<TileNode>())
                SourceNode = hit.transform.gameObject.GetComponent<TileNode>();
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
            for (int i = NavGraph.map.PathfindingTechnique.GeneratedPath.Count - 1; i > 0; i--)
            {
                TargetLocation.Add(NavGraph.map.Nodes[NavGraph.map.PathfindingTechnique.GeneratedPath[i]].transform.position);
                Speed.Add(InitialSpeed / (float)NavGraph.map.Nodes[NavGraph.map.PathfindingTechnique.GeneratedPath[i]].Cost);
            }
            TargetLocation.Add(TargetNode.transform.position);
            Speed.Add(InitialSpeed / TargetNode.Cost);
        }


        if (TargetLocation.Count > 0)
        {
         /*   for (int i = 0; i < TargetLocation.Count - 1; i++)
                Debug.DrawLine(TargetLocation[i], TargetLocation[i + 1], Color.red, 5);
    */       
    if (Vector3.Distance(TargetLocation[0], transform.position) <= 2)
            {
                transform.position = TargetLocation[0];
                Moving = false;
                TargetLocation.Remove(TargetLocation[0]);
                Speed.Remove(Speed[0]);
            }
            else
                Moving = true;

            if (Moving == true)
            {
                Vector3 Direction;
                Vector3 Normalise;
                Vector3 M;
                TargetLocation[0] = new Vector3(TargetLocation[0].x, 10, TargetLocation[0].z);
                Direction = TargetLocation[0] - transform.position;
                Normalise = Direction.normalized;
                M = Normalise * Time.deltaTime * Speed[0];
                transform.position += M;
            }
        }
        else
        {

            FoundRoute = false;
            RecievedPath = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 TextLocation = transform.position;
        TextLocation += new Vector3(-3, 13, 0);
        GUIStyle style = new GUIStyle();
        if (Friendly)
            style.normal.textColor = Color.green;
        else
            style.normal.textColor = Color.red;
        if (Military)
            style.normal.textColor = Color.blue;
        Handles.Label(TextLocation, name, style); 
    }
    void LateUpdate ()
    {
        foreach (var item in Physics.OverlapSphere(transform.position, 12))
        {
            if (item.transform.gameObject.GetComponent<TileNode>())
                SourceNode = item.transform.gameObject.GetComponent<TileNode>();
        }
        if (!FoundRoute && SourceNode != null)
        {
            TargetNode = GenerateTarget();
            FoundRoute = NavGraph.map.PathfindingTechnique.CalculateRoute(this,SourceNode, TargetNode);
        }
        MoveOnRoute();
    }

    private TileNode GenerateTarget()
    {
       return NavGraph.map.Nodes[Random.Range(0, NavGraph.map.Nodes.Count - 1)];

    }
}
