using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : MonoBehaviour
{
    /*We’re not going to run through the BFS algorithm pseudo code in
much detail because it is almost exactly the same as DFS!
 The only difference?
 Instead of a LIFO container, BFS uses a First In, First Out container for the
edges (FIFO). Commonly called a Queue (whereas DFS used a Stack)
 The rest of the algorithm is exactly the same
     */
    public NavGraph Graph; //Navigation Graph containing all nodes 
    public List<GraphNode> Route = new List<GraphNode>(); //Route that the AI took 
    public List<bool> Visited = new List<bool>(); //Nodes that the AI has visited
    public Queue<GraphEdge> graphEdges = new Queue<GraphEdge>(); //Stack of Edges
    public List<GraphNode> RandomNodes = new List<GraphNode>();
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
        graphEdges = new Queue<GraphEdge>();
        RandomNodes = new List<GraphNode>();
        Edge = new GraphEdge();
        ReachedTarget = false;
        Distance = 0;

        Moving = false;
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
        LineColor = Color.blue;
        A = 0;
        //Arty = GetComponent<GameObject>();
        Arty.transform.position = Source.gameObject.transform.position;
        ReachedTarget = false;
        for (int i = 0; i <  100; i++)
        {
            Visited.Add(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!ReachedTarget)
        {
            Visited[Source.Index] = true;

            for (int i = 0; i < Source.AdjacencyList.Count; i++)
            {
                graphEdges.Enqueue(Source.AdjacencyList[i]);
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
            for (int i = 0; i < Route.Count - 1; i++)
            {
                Debug.DrawLine(Route[i].transform.position, Route[i + 1].transform.position, LineColor);
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
            Edge = graphEdges.Dequeue();
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
                for (int i = 0; i < Edge.To.AdjacencyList.Count; i++)
                {
                    if (!Visited[Edge.To.AdjacencyList[i].To.Index])
                    {
                        graphEdges.Enqueue(Edge.To.AdjacencyList[i]);
                    }
                }
            }
        }
        return false;
    }
}
