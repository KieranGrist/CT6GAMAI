using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJALGO : MonoBehaviour
{
    /*We pass in a SourceNode and TargetNode as arguments
 We mark the Cost of the source node as 0: Cost[SourceNode] = 0.0f
 We add all adjacent edges of the source node to the Min-Priority Queue. The priority
value is acquired from the GraphEdge.GetCost method
 Now we enter a while loop (while there are elements in our queue):
 We pop an edge element off our queue and add this Edge to our list of TraversedEdges
 We check if the cost of the node this edge leads to is greater than the cost of the previous
node plus the cost of the edge: if (Cost[Edge.To] > Cost[Edge.From] + Edge.GetCost)
 If true, then change the route to this node to point to the edge source: Route[Edge.To] = Edge.From
 And set the cost of this node to the previous node’s cost plus the edge cost: Cost[Edge.To] =
Cost[Edge.From] + Edge.GetCost
 At this point you can check if you have reached the TargetNode, but it is not guaranteed that this is the
best-cost path yet. Terminating the loop here is not ideal, but we can set a boolean to mark that we
have found the target node and continue with the loop but stop the rest of this iteration:
TargetNodeFound = true
 Else we continue with the rest of the loop execution
     * 
     * 
     * Add all adjacent edges to the queue using a for loop, but check for two things
before adding:
 If the edge is already contained within the TraversedEdges list or the MinPriority Queue then do not add it to the queue
 Otherwise, add it to the Queue and set the priority as the current node’s cost
plus the cost of this adjacent edge
 End of loop
 At this point, we check if we found the target: if (TargetNodeFound ==
true)
 Found the target, so return the route as a list of node indices (same algorithm
as from the previous lecture)
*
* 
*/
    public NavGraph Graph; //Navigation Graph containing all nodes 
    public List<GraphNode> Route = new List<GraphNode>(); //Route that the AI took 
    public List<bool> Visited = new List<bool>(); //Nodes that the AI has visited
    public Queue<GraphEdge> graphEdges = new Queue<GraphEdge>(); //Stack of Edges
    public List<GraphEdge> TraveresedEdges = new List<GraphEdge>();
    public List<Vector3> TargetLocation = new List<Vector3>();
    public List<float> MinPriorirityQueue = new List<float>();
    public List<float> Cost = new List<float>();
    public Color LineColor;
    public GraphEdge Edge;
    public GraphNode Source;
    public GraphNode Target;
    public bool ReachedTarget;
    public GameObject Arty;
    float Distance;
    public float Speed;
    public bool Moving = false;

    Vector3 Direction;
    Vector3 Normalise;
    Vector3 M;


    //Dijkstra’s Algorithm
    // Start is called before the first frame update

    void Start()
    {
    }
    public void Reset()
    {
        Route.Clear();
        TargetLocation.Clear();
        TraveresedEdges.Clear();
        Cost.Clear();
        for (int i = 0; i < Graph.Nodes.Count; i++)
        {

            Cost.Add(float.MaxValue);
            Route.Add(new GraphNode());
           // TargetLocation.Add(new Vector3());



        }
    }
    // Update is called once per frame
    void Update()
    {

        if (!ReachedTarget)
        {
            Reset();
            Cost[Source.Index] = 0;
            for (int i = 0; i < Source.AdjacencyList.Count; i++)
            {
                graphEdges.Enqueue(Source.AdjacencyList[i]);
                MinPriorirityQueue.Add(Source.AdjacencyList[i].CumulativeCost);
            }
            ReachedTarget = CalculateRoute();
        }

    }
    public bool CalculateRoute()
    {
        bool TargetNodeFoundTrue = false;
        while (graphEdges.Count > 0)
        {
            Edge = graphEdges.Dequeue();
            TraveresedEdges.Add(Edge);
            if (Cost[Edge.To.Index] > Cost[Edge.From.Index] + Edge.CumulativeCost)
            {
                Route[Edge.To.Index] = Edge.From;
                Cost[Edge.To.Index] = Cost[Edge.From.Index] + Edge.CumulativeCost;
                if (Edge.To == Target)
                {
                    Route.Add(Target);
                    TargetNodeFoundTrue = true;

                }
            }
            for (int i = 0; i < Edge.To.AdjacencyList.Count; i++)
            {
                for (int b = 0; b < Graph.Nodes[i].AdjacencyList.Count; b++)
                {
                    bool RET = false;
                    for (int c = 0; c < TraveresedEdges.Count; c++)
                    {
                        if (!RET)
                        {
                            if (Graph.Nodes[i].AdjacencyList[b].ID != TraveresedEdges[c].ID)
                            {
                                RET = false;
                            }
                            else
                            {
                                RET = true;
                            }

                        }

                    }
                    for (int c = 0; c < MinPriorirityQueue.Count; c++)
                    {
                        if (!RET)
                        {
                            if (Graph.Nodes[i].AdjacencyList[b].CumulativeCost != MinPriorirityQueue[c])
                            {
                                RET = false;
                            }
                            else
                            {
                                RET = true;
                            }

                        }
          
                    }
                    if (RET == false)
                    {
                        graphEdges.Enqueue(Graph.Nodes[i].AdjacencyList[b]);
                        MinPriorirityQueue.Add(Graph.Nodes[i].AdjacencyList[b].CumulativeCost);
                    }
                }
            }
        }
        return true;
    }
}
                 