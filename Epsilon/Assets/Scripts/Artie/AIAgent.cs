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
        "Sarah"
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
    "Moss"
   };

    // Values that effect the state that the AI is in , some values that effect the state can be found in vehicle 
[Header("State Variables")]
    public float artieDrive; //Default State
 public float artieOverTake;
 public float artieDefend;
    public float artiePit;
 public float artieGoForShortCut; //If one exists
 public float artieGoForRandomItem; //If exists
public float artieAggresive;
    public float artieTimer;
    public StateMachine artieStateMachine;
    public State<AIAgent> pState;
    public string team;


    //Values that effect the pathfinding of the AI 
    public Node sourceNode;
public Node targetNode;

    //Values that control the movement of the ai
    public bool recievedPath = false;
 public List<Vector3> targetLocation = new List<Vector3>();
    List<GameObject> _pathGameObjects = new List<GameObject>();
 public   LayerMask mask;

    //Values that effect the steering of the AI

    public SteeringBehaviours sb;
    public Vehicle vehicle;

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



        sb = GetComponent<SteeringBehaviours>();
        sb.ProjectedCube = sb.GetComponentInChildren<ProjCube>();
        //    SB.ObstacleAvodienceOn();
        //   SB.WallAvodienceOn(); 


        artieStateMachine = new StateMachine();
        // ArtieStateMachine.defendState = new Defend();
        artieStateMachine.driveState = new Drive();
        //ArtieStateMachine.overtakeState = new Overtake();
        artieStateMachine.pitState = new Pit();
        //ArtieStateMachine.randomItemState = new RandomItem();
        //ArtieStateMachine.shortcutState = new Shortcut();
        pState = artieStateMachine.driveState;
        artieStateMachine.Artie = this;
        artieStateMachine.Update();
        pState.Execute(this);
    }




    private void OnDrawGizmos()
    {
        Vector3 textLocation = transform.position;
        textLocation += new Vector3(0, 1, 0);
        GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.green;        
        Handles.Label(textLocation, name, style); 
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
