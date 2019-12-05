using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
public abstract class AIAgent : MonoBehaviour { 

    //Name Lists effects what names the AI can recieve
    readonly string[] _firstNames = new string[]
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
        "Kara",
       "Hailee",
       "Helen",
       "Emily",
       "Liberty",
       "Faye",
       "Carrie",
       "Elsie",
       "Crystal",
       "Maria",
       "Ayala",
       "Alanah",
       "Amie",
       "Jack",
       "Ben",
       "Adam",
       "Tegan",
       "Edan",
       "Alison",
       "Merle",
       "Aden",
       "Allyson",
       "Lyndsey",
       "Stacia",
        "Lauren",
        "Sarah",
        "Matt",
        "Paul",
        "Maddie",
        "Lando",
        "Lewis",
        "Sebastian",
        "Carlos",
        "Sergio",
        "Piere",
        "Nicco",
        "Esteban",
        "Robert",
        "George",
        "Charles",
        "Max",
        "Alex",
        "Lance",
        "Kevin",
        "Roman",
        "Jules",
        "Peter",
        "Mikey",
        "Valtteri",
        "Daniel"


   };

    readonly string[] _lastNames = new string[]
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
    "Moss",
    "Austin",
    "Hamilton",
    "Vettel",
    "Bottas",
    "Verstappen",
    "Albon",
    "Leclerc",
    "Magnussen",
    "Grosjean",
    "Bottas",
    "Gasly",
    "Ricciardo",
    "Hulkenberg",
    "Norris",
    "Sainz",
    "Perez",
    "Stroll",
    "Raikkonen",
    "Giovinazzi",
    "Albon",
    "Kvyat",
    "Russell",
    "Kubica"
   };

    // Values that effect the state that the AI is in , some values that effect the state can be found in vehicle 
[Header("State Variables")]
    public float artieDrive; //Default State
    public float artieOverTake;
    public float artiePit;
    public StateMachine artieStateMachine;
    public State<AIAgent> pState;
    public string team;


    //Values that effect the pathfinding of the AI 
    private Node sourceNode;
    public Node targetNode;

    //Values that control the movement of the ai
    private bool recievedPath = false;
 public List<Transform> targetLocation = new List<Transform>();
 public   LayerMask mask;

    //Values that effect the steering of the AI

    public SteeringBehaviours steeringBehaviour;
    public Vehicle vehicle;

    public Node SourceNode { get => sourceNode; }
    public bool RecievedPath { get => recievedPath;}

    void Start()
    {
        if (GetComponent<Ford>())
            team = "Ford";
        if (GetComponent<Mercedes>())
            team = "Mercedes";
        if (GetComponent<Renault>())
            team = "Renault";
        if (GetComponent<Ferrari>())
            team = "Ferrari";

        if (Physics.Raycast(RaceTrack.raceTrack.FinishLine.transform.position, -transform.up, out RaycastHit item,
            float.PositiveInfinity, mask))
            targetNode = item.transform.GetComponent<Node>();
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 20.0f, mask))
        {
            var Temp = hit.collider.gameObject.GetComponent<Node>();
            if (Temp)
                sourceNode = Temp;
        }

        name = _firstNames[Random.Range(0, _firstNames.Length)] + " " + _lastNames[Random.Range(0, _lastNames.Length)];

        mask = LayerMask.GetMask("Node");

        vehicle = GetComponent<Vehicle>();



        steeringBehaviour = GetComponent<SteeringBehaviours>();
        steeringBehaviour.ProjectedCube = steeringBehaviour.GetComponentInChildren<ProjCube>();
        steeringBehaviour.ObstacleAvodienceOn();
        //   SB.WallAvodienceOn(); 
        steeringBehaviour.OvertakeOn();

        artieStateMachine = new StateMachine();
        // ArtieStateMachine.defendState = new Defend();
        artieStateMachine.driveState = new Drive();
        artieStateMachine.overtakeState = new Overtake();
        artieStateMachine.pitState = new Pit();
        //ArtieStateMachine.randomItemState = new RandomItem();
        //ArtieStateMachine.shortcutState = new Shortcut();
        pState = artieStateMachine.driveState;
        artieStateMachine.Artie = this;
    }

    public void SetSourceNode()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 20.0f, mask))
        {
            var Temp = hit.collider.gameObject.GetComponent<Node>();
            if (Temp)
                sourceNode = Temp;
        }
    }

    public void MoveOnRoute()
    {
        if (targetLocation.Count > 0)
        {
            recievedPath = true;
            steeringBehaviour.SeekOn(new Vector2(targetLocation[0].transform.position.x, targetLocation[0].transform.position.z));

            //if (targetLocation.Count > 0)
            //{
            //    Vector3 forward = targetLocation[0].forward;
            //    Vector3 toOther = transform.position - targetLocation[0].transform.position;
            //    if (Vector3.Dot(forward, toOther) >= 0) //is infront of the target node skip to next target
            //        targetLocation.Remove(targetLocation[0]);
            //}
            if (targetLocation.Count > 0)
            {
                if (Vector3.Distance(targetLocation[0].transform.position, transform.position) <= 3.325f)
                    targetLocation.Remove(targetLocation[0]);
            }


        }
        else
            recievedPath = false;
    }

    private void OnDrawGizmos()
    {
        Vector3 textLocation = transform.position;
        textLocation += new Vector3(0, 1, 0);
        GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.green;        
        Handles.Label(textLocation, name, style);
        Gizmos.color = Color.green;
        if (targetLocation.Count > 0)
            foreach (var item in targetLocation)
                Gizmos.DrawWireCube(item.transform.position + new Vector3(0, 1, 0), new Vector3(4, 1, 4));

    }
    public virtual void  Update ()
    {
        artieStateMachine.Update();
        pState.Execute(this);
        /* these are needed for any states*/
    //    Heading = vehicle.Velocity.normalized;     
        Vector3 norm = vehicle.Velocity.normalized;
        transform.forward = new Vector3(norm.x, 0, norm.y);    
    }

    
}
