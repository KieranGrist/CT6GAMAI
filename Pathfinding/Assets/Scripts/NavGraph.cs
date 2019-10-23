using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class NavGraph : MonoBehaviour
{
    public List<GraphNode> Nodes = new List<GraphNode>();
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        Nodes.Clear();
        Nodes.AddRange(FindObjectsOfType<GraphNode>());
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].name = "Node " + i;
            Nodes[i].Index = i;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
