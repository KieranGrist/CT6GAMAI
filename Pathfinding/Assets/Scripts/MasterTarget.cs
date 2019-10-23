using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTarget : MonoBehaviour
{
    public GraphNode Source;
    public List<GraphNode> RandomNodes = new List<GraphNode>();
    public NavGraph Graph;
    public GraphNode Target;
    public BFS BFSAI;
    public DFS DFSAI;
    public DJALGO DJAI;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }
    public void Reset()
    {
        RandomNodes.AddRange(FindObjectsOfType<GraphNode>());
        Source = RandomNodes[Random.Range(0, RandomNodes.Count)];
        BFSAI.transform.position = Source.transform.position;
        DFSAI.transform.position = Source.transform.position;
        DJAI.transform.position = Source.transform.position;
        Source = Target;
        Graph.Reset();

        RandomNodes.Clear();
        for (int i = 0; i < Graph.Nodes.Count; i++)
        {
            if (Graph.Nodes[i] != Source)
            {
                RandomNodes.Add(Graph.Nodes[i]);
            }
        }

        BFSAI.Target = Target;
        BFSAI.Source = Source;
        DFSAI.Target = Target;  
        DFSAI.Source = Source;
        DJAI.Target = Target;
        DJAI.Source = Source;
        BFSAI.Reset();
        DFSAI.Reset();
        DJAI.Reset();
        RandomNodes.Clear();
        Target = RandomNodes[Random.Range(0, RandomNodes.Count)];
    }
    // Update is called once per frame
    void Update()
    {
        if (BFSAI.A >= BFSAI.Route.Count && DFSAI.A >= DFSAI.Route.Count)
        {
            Source = Target;
          
            for (int i = 0; i < Graph.Nodes.Count; i++)
            {
                if (Graph.Nodes[i] != Target)
                {
                    RandomNodes.Add(Graph.Nodes[i]);
                }
            }

            BFSAI.Target = Target;
            BFSAI.Source = Source;
            DFSAI.Target = Target;
            DFSAI.Source = Source;
            DJAI.Target = Target;
            DJAI.Source = Source;
            BFSAI.Reset();
            DFSAI.Reset();
            DJAI.Reset();
            RandomNodes.Clear();
            Target = RandomNodes[Random.Range(0, RandomNodes.Count)];
        }

    }
}
