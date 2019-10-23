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
     
        for (int i =0; i < Nodes.Count; i++)
        {
            Nodes[i].Index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
