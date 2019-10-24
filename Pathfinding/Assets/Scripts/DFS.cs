using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DFS : MonoBehaviour
{
    //*Depth First Search algrotium searches all avaialable nodes and creates a path, it does not care how effective said path is and will use the first availabile path towards it
    public NavGraph Graph; //Navigation Graph containing all nodes 
    public List<GraphNode> Route = new List<GraphNode>(); //Route that the AI took 
    public List<GraphNode> Path = new List<GraphNode>();
    public List<bool> Visited = new List<bool>(); //Nodes that the AI has visited
    public Stack<GraphEdge> graphEdges = new Stack<GraphEdge>(); //Stack of Edges
    public Color LineColor;
    public GraphEdge Edge;
    public GraphNode Source;
    public GraphNode Target;
    public bool ReachedTarget;
    bool FoundRoute;
    float Timer;
    public void Reset()
    {
        Route = new List<GraphNode>(); //Route that the AI took 
        Visited = new List<bool>(); //Nodes that the AI has visited
        graphEdges = new Stack<GraphEdge>(); //Stack of Edges
        LineColor = Color.red;
        Edge = new GraphEdge();
        ReachedTarget = false;
        for (int i = 0; i < Graph.Nodes.Count; i++)
        {
            Visited.Add(false);
            Route.Add(new GraphNode());
        }

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
            Route[Source.Index] = Source;
            for (int i = 0; i < Source.AdjacencyList.Count; i++)
            {
                graphEdges.Push(Source.AdjacencyList[i]);
            }
            CalculateRoute();
            ReachedTarget = FoundRoute;
        
            if (ReachedTarget)
            {
                Path.Clear();
                Path.Add(Target);
                GraphNode currentNode = Target;
                Path.Add(currentNode);
                for (int i = 0; i < Route.Count; i++)
                {
                    currentNode = Route[i];
                    Path.Add(currentNode);
                }

            }
        }
        else
        {
            Timer += Time.deltaTime;
            if (Timer > 20)
            {
                for (int i = 0; i < Path.Count - 1; i++)
                {
                    Debug.DrawLine(Path[i].transform.position, Path[i + 1].transform.position, Color.green, 2.0f);

                }
                for (int i = 0; i < Route.Count - 1; i++)
                {
                    Debug.DrawLine(Route[i].transform.position, Route[i + 1].transform.position, Color.black, 2.0f);

                }
            }
        }

    }
    public void CalculateRoute()
    {
        Timer = 0;
        float Col = 0;
        FoundRoute = false;
        while (graphEdges.Count > 0)
        {
            
            Edge = graphEdges.Pop();
            Debug.DrawLine(Edge.From.transform.position, Edge.To.transform.position, new Color(Col, 0,0) ,20.0f);
            Col += 0.05f;
            // Route[Edge.To] = Edge.From;
            Route[Edge.To.Index] = (Edge.From);
            Visited[Edge.To.Index] = true;
            
            if (Edge.To == Target)
            {              
                 FoundRoute = true;
              
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
        
    }
}
