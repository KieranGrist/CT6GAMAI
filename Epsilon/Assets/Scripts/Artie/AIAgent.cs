using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
/// <summary>
/// Abstract class which controlls the AI agent behavior
/// Needs to be inherited by driver profiles
/// </summary>
 public abstract class AIAgent : MonoBehaviour { 

/// <summary>
/// First Name List
/// </summary>
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
    /// <summary>
    /// Last Name List
    /// </summary>
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
    /// <summary>
    /// State variable which effects Driving
    /// </summary>
    public float artieDrive; //Default State
                                /// <summary>
                                /// State variable which effects overtaking
                                /// </summary>
    public float artieOverTake;
    /// <summary>
    /// State variable which effects Pitting
    /// </summary>
    public float artiePit;
     StateMachine artieStateMachine; //AIS state machine 
  public   State<AIAgent> pState; //Current state it is in
    public string team; //What team it is in 


    //Values that effect the pathfinding of the AI 
    private Node sourceNode; //Where the AI is
    public Node targetNode; //Where the AI is going to

    //Values that control the movement of the ai
    private bool recievedPath = false; //If the ai has recieved a path 
    public List<Transform> targetLocation = new List<Transform>(); //List of waypoints to drive to 
    public LayerMask mask; //Mask used in collision detection

    //Values that effect the steering of the AI

    public SteeringBehaviours steeringBehaviour; //Ais steering behaviors
    public Vehicle vehicle; //Ais vehicle 
                            /// <summary>
                            /// Get the current source node
                            /// </summary>
    public Node SourceNode { get => sourceNode; }
    /// <summary>
    /// Check if the AI has recieved a path
    /// </summary>
    public bool RecievedPath { get => recievedPath;}
    //Get Artie Drive State Variable
    public float ArtieDrive { get => artieDrive; }
    //Get Artie Overtake Variable
    public float ArtieOverTake { get => artieOverTake;  }
    //Get Artie Pit Variable
    public float ArtiePit { get => artiePit; }

    void Start()
    {
        //Set the AIS team
        if (GetComponent<Ford>())  //If AI has ford vehicle
            team = "Ford";  //Team = ford
        if (GetComponent<Mercedes>()) //If ai has mercedes vehicle
            team = "Mercedes"; //Team = mercedes
        if (GetComponent<Renault>()) //If ai has renault vehicle
            team = "Renault"; //Team = renault 
        if (GetComponent<Ferrari>()) //If ai = ferrari vehicle
            team = "Ferrari"; //Team = Ferrari

        if (Physics.Raycast(RaceTrack.raceTrack.FinishLine.transform.position, -transform.up, out RaycastHit item,
            float.PositiveInfinity, mask)) //Raycast from the finish line down, if it hits
            targetNode = item.transform.GetComponent<Node>(); //Set target node to hit node
        SetSourceNode(); //Call Set Source Function

        name = _firstNames[Random.Range(0, _firstNames.Length)] + " " + _lastNames[Random.Range(0, _lastNames.Length)]; //Create a random name from the list

        mask = LayerMask.GetMask("Node"); //Set the collison mask to be nodes 

        vehicle = GetComponent<Vehicle>(); //Get the current vehicle component 



        steeringBehaviour = GetComponent<SteeringBehaviours>(); //Get the steering behavior 
        steeringBehaviour.ProjectedCube = steeringBehaviour.GetComponentInChildren<ProjCube>(); //Set the steering behaviors projected cuge
        steeringBehaviour.ObstacleAvodienceOn(); //Turn on Obstacle Avoidence


        artieStateMachine = new StateMachine(); //Create a new state machine

        artieStateMachine.driveState = new Drive(); //Create a new drive state
        artieStateMachine.overtakeState = new Overtake(); // Create a new overtake state 
        artieStateMachine.pitState = new Pit(); //Create a new pit state
         
        pState = artieStateMachine.driveState; //set the current state to be driving 
        artieStateMachine.Artie = this; //Set the state machine reference of artie to be this
    }
    /// <summary>
    /// Raycast down from current position and get the node it hits then set it to be the source node
    /// </summary>
    public void SetSourceNode()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 20.0f, mask)) //raycast down from arties position
        {
            var Temp = hit.collider.gameObject.GetComponent<Node>(); //Get the node component from what has been hit
            if (Temp)//If node exists 
                sourceNode = Temp;//Set source node to be temp node
        }
    }
    /// <summary>
    /// Move along a given target location route
    /// </summary>
    public void MoveOnRoute()
    {
        if (targetLocation.Count > 0) //If the targets exiss
        {
            recievedPath = true; //Recieved path is true
            steeringBehaviour.SeekOn(new Vector2(targetLocation[0].transform.position.x, targetLocation[0].transform.position.z)); //Seek to the closest target which will always be 0
  
                if (Vector3.Distance(targetLocation[0].transform.position, transform.position) <= 3.325f) //If close enough
                    targetLocation.Remove(targetLocation[0]); //Remove target location
        }
        else
            recievedPath = false; //Set recieved path to false as no path exists
    }

    private void OnDrawGizmos()
    {
        Vector3 textLocation = transform.position; //Create and set the text location to be the current AIS position
        textLocation += new Vector3(0, 1, 0); //Increase its height by 1
        GUIStyle style = new GUIStyle(); //Create a new style for the text 
            style.normal.textColor = Color.green;        //Set the colour to be green
        Handles.Label(textLocation, name, style); //Create a label for the AI this is used to identify them
        Gizmos.color = Color.green; //Set gizmo location to be true
        if (targetLocation.Count > 0) //If target location exist 
            foreach (var item in targetLocation) //Loop through each target location
                Gizmos.DrawWireCube(item.transform.position + new Vector3(0, 1, 0), new Vector3(4, 1, 4)); //Draw a wire cube on their location

    }
  public   virtual void  Update ()
    {
        artieStateMachine.Update(); //Update the state machine 
        pState.Execute(this); //Execute the highest desire 
  
        Vector3 norm = vehicle.Velocity.normalized; //set norm to be the velocity normalised 
        transform.forward = new Vector3(norm.x, 0, norm.y);    //Set the transform forward to be that of norm
    }

    
}
