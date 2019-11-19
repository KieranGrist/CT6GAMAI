using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(SteeringBehaviours))]
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
    public TileNode SourceNode;
    public TileNode TargetNode;
    List<GameObject> PathGameObjects = new List<GameObject>();
    public bool RecievedPath = false;
    bool Moving = false;
    List<Vector3> TargetLocation = new List<Vector3>();
    float InitialSpeed = 250.0f;
    public LayerMask Mask;
    List<float> Speed = new List<float>();
    bool ResetAllNodes;
    bool FoundRoute;

   
    [Header("Steering Behaviors")]
    public Vector2 Velocity;

    //Position, Heading and Side can be accessed from the transform component with transform.position, transform.forward and transform.right respectively

    //"Constant" values, they are public so we can adjust them through the editor

    //Represents the weight of an object, will effect its acceleration
    public float Mass = 1;

    //The maximum speed this agent can move per second
    public float MaxSpeed = 1;

    //The thrust this agent can produce
    public float MaxForce = 1;
    public float MaxTurnRate = 1.0f;
    public Vector2 SteeringForce;
    public SteeringBehaviours SB;
    public Vector2 Heading;
    public Vector2 Side;
    void Start()
    {
        SB = GetComponent<SteeringBehaviours>();
        SB.ObstacleAvodienceOn();
        SB.ProjectedCube = SB.GetComponentInParent<ProjCube>();
        Friendly = Random.value >= 0.5;
        Military = Random.value >= 0.5;
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
        SB.SeekOn(new Vector2(TargetNode.transform.position.x, TargetNode.transform.position.z));
        SteeringForce = SB.Calculate();
        Vector2 Acceleration = SteeringForce / Mass;
        Velocity += Acceleration;

        Velocity = Vector2.ClampMagnitude(Velocity, MaxSpeed);
        Heading = Velocity.normalized;
        if (Velocity != Vector2.zero)
        {
            transform.position += new Vector3(Velocity.x, 0, Velocity.y) * Time.deltaTime;
            Vector3 norm = Velocity.normalized;
            transform.forward = new Vector3(norm.x, 0, norm.y);
        }
        if (TargetLocation.Count > 0)
        {

            if (Vector3.Distance(TargetLocation[0], transform.position) <= 2)
            {
                transform.position = TargetLocation[0];
                Moving = false;
                TargetLocation.Remove(TargetLocation[0]);
                Speed.Remove(Speed[0]);
            }

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
            GetComponent<Renderer>().material.color = Color.blue;
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
        TileNode RandomNode;
        do
            RandomNode = NavGraph.map.Nodes[Random.Range(0, NavGraph.map.Nodes.Count - 1)];
        while ((!Military && (RandomNode.GetComponent<MilitaryAirport>()|| RandomNode.GetComponent<MilitaryBase>())) || RandomNode == SourceNode);
        if (!RandomNode.Walkable)
            RandomNode = NavGraph.map.Nodes[Random.Range(0, NavGraph.map.Nodes.Count - 1)];
        return RandomNode;

    }
}
