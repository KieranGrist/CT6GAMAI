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
    public string team;


    //Values that effect the pathfinding of the AI 
    private Node sourceNode;
    public Node targetNode;

    //Values that control the movement of the ai
    private bool recievedPath = false;
 public List<Transform> targetLocation = new List<Transform>();
 public   LayerMask mask;

    //Values that effect the steering of the AI


    public Node SourceNode { get => sourceNode; }
    public bool RecievedPath { get => recievedPath;}

    void Start()
    {
    

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 20.0f, mask))
        {
            var Temp = hit.collider.gameObject.GetComponent<Node>();
            if (Temp)
                sourceNode = Temp;
        }

        name = _firstNames[Random.Range(0, _firstNames.Length)] + " " + _lastNames[Random.Range(0, _lastNames.Length)];

        mask = LayerMask.GetMask("Node");


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
   
    }

    
}
