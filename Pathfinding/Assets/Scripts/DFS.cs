using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DFS : MonoBehaviour
{
    /*The DFS algorithm will need to create two lists of equivalent size to
the amount of nodes (pre-initializing this only once is a good idea)
 list of integers which stores the Route from one node to the other
 list of Booleans which stores whether a node has been Visited or not
 Remember our graph has a list of nodes?
 Node n will correspond with index n in both of these lists
     * 
     * 
     * 
     */
    /*
     * Next we are going initialise a stack of Graph Edges
 A stack is a type of Last In, First Out (LIFO) sequential container (like
a list)
 The last element to get added to a stack is the first one to get out
 Next let’s look at the actual algorithm...
*/

    /*
     * We pass in a SourceNode and TargetNode as arguments
     We add all adjacent edges of the source node to the stack and mark the source
    node as visited
     We will enter a while loop (while there are elements in our stack):
     We pop an edge element off the stack
     We find which node index this edge leads to (Edge.To) and change our route element at the
    same index to point to the edge source (in code: Route[Edge.To] = Edge.From)
     Mark that node as visited (Visited[Edge.To] = true)
     If Edge.To == TargetNode
     Exit the function, returning true because we found our target
     Otherwise, add all new adjacent edges to the stack, checking to make sure each one has
    not been visited yet (use a for loop)
     End of loop
     If the loop has ended and reached this point, it means no path to the target was
    found
    */
    public NavGraph Graph;
    public List<GraphNode> Route = new List<GraphNode>();
    public List<GraphNode> Path = new List<GraphNode>();
    public List<bool> Visited = new List<bool>();
    public Stack<GraphEdge> graphEdges = new Stack<GraphEdge>();
    public List<GraphNode> RandomNodes = new List<GraphNode>();
    public GraphEdge Edge;
    public GraphNode Source;
    public GraphNode Target;
    bool ReachedTarget;
    public GameObject Arty;
    float Distance;
    public float Speed;
    public bool Moving = false;
    public List<Vector3> TargetLocation = new List<Vector3>();
    Vector3 Direction;
    Vector3 Normalise;
    Vector3 M;
    public int A;
  
    // Start is called before the first frame update
    void Start()
    {
        A = 0;
        //Arty = GetComponent<GameObject>();
        Arty.transform.position = Source.gameObject.transform.position;
        ReachedTarget = false;
        for (int i = 0; i < Graph.Nodes.Count + 2; i++)
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
            for (int i = 0; i < Route.Count - 1; i++)
            {
                Debug.DrawLine(Route[i].transform.position, Route[i + 1].transform.position);
            }
            Distance = Vector3.Distance(TargetLocation[A], transform.position);
            if (transform.position != TargetLocation[A])
            {
                if (Distance <= 0.05)
                {
                    transform.position = TargetLocation[A];
                    Moving = false;
                    A++;
                }
                Moving = true;

            }
            else
            {
                A++;
                Moving = false;
            }

            if (Moving == true)
            {
                // direction, normalise, direction * DeltaTime and speed, add that to current location
                Direction = TargetLocation[A] - transform.position;
                Normalise = Direction.normalized;
                M = Normalise * Time.deltaTime * Speed;

                Arty.transform.position += M;
            }

            
            if (A >= Route.Count)
            {
                      
Route = new List<GraphNode>();
Path = new List<GraphNode>();
  Visited = new List<bool>();
graphEdges = new Stack<GraphEdge>();
RandomNodes = new List<GraphNode>();
Edge;
 Source;
 Target;
 ReachedTarget;
Arty;
 Distance;
 Speed;
 Moving = false;
TargetLocation = new List<Vector3>();
Direction;
     Normalise;
     M;
      A;
                for (int i = 0; i < Graph.Nodes.Count; i++)
                {
                    if (Graph.Nodes[i] != Target)
                    {
                        RandomNodes.Add(Graph.Nodes[i]);
                    }
                }
                for (int i = 0; i < Graph.Nodes.Count + 2; i++)
                {
                    Visited.Add(false);
                }
                rando
                ReachedTarget = false;
                Route = new List<GraphNode>();
                Path = new List<GraphNode>();
                Visited = new List<bool>();
                graphEdges = new Stack<GraphEdge>();
                TargetLocation = new List<Vector3>();
                Source = Target;
                Target = RandomNodes[Random.Range(0, RandomNodes.Count)];
             
                A = 0;
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
            Debug.DrawLine(Edge.From.transform.position, Edge.To.transform.position);
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
