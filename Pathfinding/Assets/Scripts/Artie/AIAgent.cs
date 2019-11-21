﻿using System.Collections;
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
    [Header("Affinities")]
    public bool Military;
    public bool Friendly = false;
    [Header("Pathfinding")]
    public TileNode SourceNode;
    public TileNode TargetNode;

    [Header("Movement")]
    public bool RecievedPath = false;
    public List<Vector3> TargetLocation = new List<Vector3>();
    List<GameObject> PathGameObjects = new List<GameObject>();

    bool FoundRoute;

    [Header("Collision Detection")]
    public LayerMask Mask;

    [Header("Steering Behaviors")]
    public SteeringBehaviours SB;

    void Start()
    {
        SB = GetComponent<SteeringBehaviours>();
        SB.ObstacleAvodienceOn();
        SB.WallAvodienceOn();
        SB.ProjectedCube = SB.GetComponentInChildren<ProjCube>();
        Friendly = Random.value >= 0.5;
        Military = false;
        // Military = Random.value >= 0.5;
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
            RecievedPath = true;
            for (int i = NavGraph.map.PathfindingTechnique.GeneratedPath.Count - 1; i > 0; i--)
            {
                TargetLocation.Add(NavGraph.map.Nodes[NavGraph.map.PathfindingTechnique.GeneratedPath[i]].transform.position);                    
            }
            TargetLocation.Add(TargetNode.transform.position);
        }

        if (TargetLocation.Count > 0)
        {
            SB.SeekOn(new Vector2(TargetLocation[0].x, TargetLocation[0].z));
            if (Vector3.Distance(TargetLocation[0], transform.position) <= 3)
                TargetLocation.Remove(TargetLocation[0]);
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
            GetComponent<Renderer>().material.color = Color.blue;
        Handles.Label(TextLocation, name, style); 
    }
    void Update ()
    {
        foreach (var item in Physics.OverlapSphere(transform.position, 12))
        {
            if (item.gameObject.GetComponent<TileNode>())
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
        while ((!Military && RandomNode.Military) || RandomNode == SourceNode);
        if (!RandomNode.Walkable)
            RandomNode = NavGraph.map.Nodes[Random.Range(0, NavGraph.map.Nodes.Count - 1)];
        return RandomNode;

    }
}
