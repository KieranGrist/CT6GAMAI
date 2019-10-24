using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNav : MonoBehaviour
{
    public GameObject Node;
    public NavGraph graph;
    public DFS DepthAI;
    public GraphNode SourceNode, TargetNode;
    GraphNode PreviousNode;
    public int Area;
    int PreviousArea = -15000;
    public List<GameObject> gameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
      
        PreviousNode = TargetNode;
        if (Area != PreviousArea)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                Destroy(gameObjects[i]);
            }
            gameObjects.Clear();
            for (int x = 0 - Area; x < Area; x += 2)
            {
                for (int z = 0 - Area; z < Area; z += 2)
                {
                    GameObject GO = Instantiate(Node);
                    GO.transform.position = new Vector3(x, 3, z);
                    gameObjects.Add(GO);
                }
            }
            List<GraphNode> nodes = new List<GraphNode>();
            nodes.AddRange(FindObjectsOfType<GraphNode>());
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Reset();
            }
            SourceNode = gameObjects[0].GetComponent<GraphNode>();
            SourceNode.GetComponent<Renderer>().material.SetColor("_Color",Color.green);
            TargetNode = gameObjects[gameObjects.Count-1].GetComponent<GraphNode>();
            TargetNode.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            graph.Reset(); DepthAI.Reset();
            DepthAI.Source = SourceNode;
            DepthAI.Target = TargetNode;

        }
        TargetNode = PreviousNode;
        PreviousArea = Area;
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetNode != PreviousNode)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            }

                SourceNode = gameObjects[0].GetComponent<GraphNode>();
            SourceNode.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
          //  TargetNode = gameObjects[gameObjects.Count - 1].GetComponent<GraphNode>();
            TargetNode.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
          //  graph.Reset();
            DepthAI.Reset();
            DepthAI.Source = SourceNode;
            DepthAI.Target = TargetNode;

        }
        TargetNode = PreviousNode;
    }
}
