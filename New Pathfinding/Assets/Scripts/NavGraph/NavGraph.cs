    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavGraph : MonoBehaviour
{
    //Pipeline : Create Node Tile Map ->  Generate Racetrack -> Generate Obstabcles -> Selector -> Generate Nav Mash - > Place AI units -> AI units select personality -> Place Player -> AI Unit Pathfinding -> Play!

    public List<Node> Nodes = new List<Node>();
    public ASTAR PathfindingTechnique;
    public static NavGraph map;
    public bool ResetMap;
    void Start  ()
    {
        ResetMap = true;
        AddNodes();
        map = this;
        DontDestroyOnLoad(gameObject);
    }
    void AddNodes()
    {
        Nodes.Clear();
        Nodes.AddRange(FindObjectsOfType<Node>());
        for (int i = 0; i < Nodes.Count; i++)
            Nodes[i].Index = i;
    }

    // Update is called once per frame
    void Update()
    {
        map = this;
        if (ResetMap)
        {
            ResetAllNodes();
            ResetMap = false;
        }

    }

    public void ResetAllNodes()
    {
        AddNodes();
        foreach (var item in Nodes)
            item.Reset();
    }
}
