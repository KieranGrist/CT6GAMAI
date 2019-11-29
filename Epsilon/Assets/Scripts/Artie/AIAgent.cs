using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public abstract class AIAgent : MonoBehaviour { 

    //Name Lists effects what names the AI can recieve
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
        "Lauren",
        "Sarah"
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
    "Ryan",
    "Williams",
    "Griffiths",
    "Moss"
   };

    // Values that effect the state that the AI is in  
    [Header("State Variables")]
    float AgentHealth = 100;
    bool AggresiveNature = false;

    //Values that effect the pathfinding of the AI 
    [Header("Pathfinding")]
    public Node SourceNode;
    public Node TargetNode;
    public Stack<Node> SectorNodes = new Stack<Node>();

    //Values that control the movement of the ai
    [Header("Movement")]
    public bool RecievedPath = false;
    public List<Vector3> TargetLocation = new List<Vector3>();
    List<GameObject> PathGameObjects = new List<GameObject>();
    LayerMask Mask;

    //Values that effect the steering of the AI
    [Header("Steering Behaviors")]
    public Vector2 SteeringForce;
    public SteeringBehaviours SB;
    public Vector2 Heading;
    public Vector2 Side;
    public Vector2 Velocity;
    public Vehicle vehicle;
    void Start()
    {

        Mask = LayerMask.GetMask("Node");
        SB = GetComponent<SteeringBehaviours>();
        //SB.ObstacleAvodienceOn();
        //SB.WallAvodienceOn();
      SB.ProjectedCube = SB.GetComponentInChildren<ProjCube>(); 

        string ObjectName = name = FirstNames[Random.Range(0, FirstNames.Length)] + " " + LastNames[Random.Range(0, LastNames.Length)];
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mask))
            if (hit.transform.gameObject.GetComponent<Node>())
                SourceNode = hit.transform.gameObject.GetComponent<Node>();
    }
 public void MoveOnRoute()
    {
        if (!RecievedPath)
        {

            if (SourceNode && TargetNode)
            {
                TargetLocation.Clear();
                TargetLocation = new List<Vector3>();
                RecievedPath = true;
                var Path = ASTAR.CalculatePath(SourceNode, TargetNode);
                if (Path.Count > 0)
                    for (int i = Path.Count - 1; i > 0; i--)
                    {
                        TargetLocation.Add(NavGraph.map.Nodes[Path[i]].transform.position);
                    }
                TargetLocation.Add(TargetNode.transform.position);
            }
        }

        if (TargetLocation.Count > 0)
        {
            SB.SeekOn(new Vector2(TargetLocation[0].x, TargetLocation[0].z));
            if (Vector3.Distance(TargetLocation[0], transform.position) <= 2)
                TargetLocation.Remove(TargetLocation[0]);
        }
        else
        {
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 20.0f, Mask))
            {
                var Temp = hit.collider.gameObject.GetComponent<Node>();
                if (Temp)
                    SourceNode = Temp;
            }
            TargetNode = NextTarget(); 
             
            RecievedPath = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 TextLocation = transform.position;
        TextLocation += new Vector3(0, 1, 0);
        GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.green;        
        Handles.Label(TextLocation, name, style); 
    }
    void Update ()
    {
        MoveOnRoute();
        SteeringForce = SB.Calculate();
        Vector2 Acceleration = SteeringForce / vehicle.Mass;
        Velocity += Acceleration;

        Velocity = Vector2.ClampMagnitude(Velocity, vehicle.MaxSpeed);
        Heading = Velocity.normalized;
        if (Velocity != Vector2.zero)
        {
            transform.position += new Vector3(Velocity.x, 0, Velocity.y) * Time.deltaTime;
            Vector3 norm = Velocity.normalized;
            transform.forward = new Vector3(norm.x, 0, norm.y);
        }
    }

    private Node NextTarget()
    {
        /* Statck is LIFO
         * Finish Line
         * Sector 1
         * Sector 2
         * Sector 3
         */
         if (SectorNodes.Count <= 0)
        {
            if (Physics.Raycast(RaceTrack.raceTrack.Sector3.transform.position, -transform.up, out RaycastHit hit, float.PositiveInfinity, Mask))
                SectorNodes.Push(hit.transform.GetComponent<Node>());
            if (Physics.Raycast(RaceTrack.raceTrack.Sector2.transform.position, -transform.up, out  hit, float.PositiveInfinity, Mask))
                SectorNodes.Push(hit.transform.GetComponent<Node>());
            if (Physics.Raycast(RaceTrack.raceTrack.Sector1.transform.position, -transform.up, out  hit, float.PositiveInfinity, Mask))
                SectorNodes.Push(hit.transform.GetComponent<Node>());
            if (Physics.Raycast(FinishLine.finishLine.transform.position, -transform.up, out  hit, float.PositiveInfinity, Mask))
                SectorNodes.Push(hit.transform.GetComponent<Node>());
        }
        return SectorNodes.Pop();
    }
}
