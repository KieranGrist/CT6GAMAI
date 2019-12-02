using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]

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

    // Values that effect the state that the AI is in , some values that effect the state can be found in vehicle 
    [Header("State Variables")]
    public float Artie_Drive; //Default State
    public float Artie_OverTake;
    public float Artie_Defend;
    public float Artie_Pit;
    public float Artie_GoForShortCut; //If one exists
    public float Artie_GoForRandomItem; //If exists
    public float Artie_Aggresive;

    public StateMachine ArtieStateMachine;
    public State<AIAgent> pState;
    public string Team;


    //Values that effect the pathfinding of the AI 
    [Header("Pathfinding")]
    public Node SourceNode;
    public Node TargetNode;

    //Values that control the movement of the ai
    [Header("Movement")]
    public bool RecievedPath = false;
    public List<Vector3> TargetLocation = new List<Vector3>();
    List<GameObject> PathGameObjects = new List<GameObject>();
 public   LayerMask Mask;

    //Values that effect the steering of the AI
    [Header("Steering Behaviors")]
    public SteeringBehaviours SB;
    public Vehicle vehicle;

    void Start()
    {
        if (GetComponent<Ford>())
            Team = "Ford";
            if (GetComponent<Mercedes>())
            Team = "Mercedes";
        if (GetComponent<Renault>())
            Team = "Renault";
        if (GetComponent<Ferrari>())
            Team = "Ferrari";

        if (Physics.Raycast(RaceTrack.raceTrack.FinishLine.transform.position, -transform.up, out RaycastHit item, float.PositiveInfinity, Mask))
          TargetNode = item.transform.GetComponent<Node>();
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 20.0f, Mask))
        {
            var Temp = hit.collider.gameObject.GetComponent<Node>();
            if (Temp)
                SourceNode = Temp;
        }

        name = FirstNames[Random.Range(0, FirstNames.Length)] + " " + LastNames[Random.Range(0, LastNames.Length)];

        Mask = LayerMask.GetMask("Node");

        vehicle = GetComponent<Vehicle>();



        SB = GetComponent<SteeringBehaviours>();
        SB.ProjectedCube = SB.GetComponentInChildren<ProjCube>();
    //    SB.ObstacleAvodienceOn();
     //   SB.WallAvodienceOn(); 


        ArtieStateMachine = new StateMachine();
        // ArtieStateMachine.defendState = new Defend();
        ArtieStateMachine.driveState = new Drive();
        //ArtieStateMachine.overtakeState = new Overtake();
        //ArtieStateMachine.pitState = new Pit();
        //ArtieStateMachine.randomItemState = new RandomItem();
        //ArtieStateMachine.shortcutState = new Shortcut();
        pState = ArtieStateMachine.driveState;
        ArtieStateMachine.Artie = this;
        ArtieStateMachine.Update();
        pState.Execute(this);
    }


  

    private void OnDrawGizmos()
    {
        Vector3 TextLocation = transform.position;
        TextLocation += new Vector3(0, 1, 0);
        GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.green;        
        Handles.Label(TextLocation, name, style); 
    }
    public virtual void  Update ()
    {
        ArtieStateMachine.Update();
        pState.Execute(this);
        /* these are needed for any states*/
    //    Heading = vehicle.Velocity.normalized;     
        Vector3 norm = vehicle.Velocity.normalized;
        transform.forward = new Vector3(norm.x, 0, norm.y);    
    }

    
}
