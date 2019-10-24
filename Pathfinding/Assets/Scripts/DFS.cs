using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DFS : MonoBehaviour
{
    //*Depth First Search algrotium searches all avaialable nodes and creates a path, it does not care how effective said path is and will use the first availabile path towards it
    public NavGraph Graph; //Navigation Graph containing all nodes 
    public List<GraphNode> Route = new List<GraphNode>(); //Route that the AI took 
    public List<bool> Visited = new List<bool>(); //Nodes that the AI has visited
    public Stack<GraphEdge> graphEdges = new Stack<GraphEdge>(); //Stack of Edges
    public Color LineColor;
    public GraphEdge Edge;
    public GraphNode Source;
    public GraphNode Target;
    public bool ReachedTarget;
    public GameObject Arty;
    float Distance;
    public float Speed;
    public bool Randomnise = true;
    public bool Moving = false;
    public List<Vector3> TargetLocation = new List<Vector3>();
    Vector3 Direction;
    Vector3 Normalise;
    Vector3 M;
    public int A;
    public void Reset()
    {
        Route = new List<GraphNode>();
        Visited = new List<bool>();
        graphEdges = new Stack<GraphEdge>();
        Edge = new GraphEdge();
        ReachedTarget = false;
        Distance = 0;
        LineColor = Color.red;
        A = 0;
        //Arty = GetComponent<GameObject>();
        Arty.transform.position = Source.gameObject.transform.position;
        ReachedTarget = false;
        for (int i = 0; i < Graph.Nodes.Count; i++)
        {
            Visited.Add(false);
            Moving = false;
        }
        TargetLocation = new List<Vector3>();
        Direction = new Vector3();
        Normalise = new Vector3();
        M = new Vector3();
        A = 0;
        for (int i = 0; i < Graph.Nodes.Count; i++)
        {
            Visited.Add(false);
        }
        ReachedTarget = false;
        Route = new List<GraphNode>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!ReachedTarget)
        {
            Visited[Source.Index] = true;

            for (int i = 0; i < Source.AdjacencyList.Count; i++)
            {
                graphEdges.Push(Source.AdjacencyList[i]);
            }
            ReachedTarget = CalculateRoute();
            if (ReachedTarget)
            {
                for (int i = 0; i < Route.Count; i++)
                {
                    TargetLocation.Add(Route[i].transform.position);

                }
            }
        }
        else
        {
            if (Route.Count > 2)
            {
                for (int i = 0; i < Route.Count - 1; i++)
                {
                    if (Source != null && Route != null)
                    {
                        Debug.DrawLine(Source.transform.position, Route[0].transform.position);


                        Debug.DrawLine(Route[i].transform.position, Route[i + 1].transform.position, LineColor);
                    }
                }
            }
            if (A < Route.Count)
            {
                Distance = Vector3.Distance(TargetLocation[A], transform.position);

                if (Distance <= 0.05)
                {
                    transform.position = TargetLocation[A];
                    Moving = false;
                    A++;
                }
                else
                {
                    Moving = true;
                }
            }
            if (Moving == true)
            {
                // direction, normalise, direction * DeltaTime and speed, add that to current location
                Direction = TargetLocation[A] - transform.position;
                Normalise = Direction.normalized;
                M = Normalise * Time.deltaTime * Speed;

                Arty.transform.position += M;
            }
        
        }
    }

    public bool CalculateRoute()
    {
        while (graphEdges.Count > 0)
        {
            Edge = graphEdges.Pop();
            // Route[Edge.To] = Edge.From;
            Route.Add(Edge.From);
            Visited[Edge.To.Index] = true;
            
            if (Edge.To == Target)
            {
                Route.Add(Target);
                return true;
               
            }
            else
            {
                for (int i =0; i < Edge.To.AdjacencyList.Count; i ++)
                {
                    if (!Visited[Edge.To.AdjacencyList[i].To.Index])
                    {
                        graphEdges.Push(Edge.To.AdjacencyList[i]);
                    }
                }
            }
        }
        return false;
    }
}
