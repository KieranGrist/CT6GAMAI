using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    List<GameObject> gameObjects = new List<GameObject>();
    public List<Node> Nodes = new List<Node>(); //List of the nodes
    public static Graph map; //Static reference towards the map
    private void Awake()
    {
        map = this;
    }
    private void Update()
    {
        map = this;
    }
    public void GenerateGraph()
    {
        Nodes.Clear(); //Clear nodes

        for (int x = -50; x < 50; x++)
            for (int z = -50; z < 50; z++)
            {
                GameObject go = new GameObject();
                go.transform.position = new Vector3(x, 0, z);
                go.transform.localScale = new Vector3(1, 1, 1);
                go.AddComponent<Node>();
            }

        Nodes.AddRange(FindObjectsOfType<Node>()); //Find all nodes in the game world and add them to this nav graph
        for (int i = 0; i < Nodes.Count; i++) //Loop through each ndoe 
            Nodes[i].GenerateNeighbours(); //Reset the node

    }
}
