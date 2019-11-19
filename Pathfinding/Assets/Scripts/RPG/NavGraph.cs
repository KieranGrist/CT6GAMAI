using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NavGraph : MonoBehaviour
{
    public List<TileNode> Nodes = new List<TileNode>();
   public Pathfinder PathfindingTechnique;   
    LayerMask TileMask;
    public static NavGraph map;

    void Awake()
    {
        AddNodes();
        map = this;
      DontDestroyOnLoad(gameObject);
        PathfindingTechnique = new ASTAR();
     
    }
    void AddNodes()
    {
        Nodes.Clear();
        Nodes.AddRange(FindObjectsOfType<TileNode>());
        for (int i = 0; i < Nodes.Count; i++)
            Nodes[i].Index = i;
    }
   
    // Update is called once per frame
   void Update()
    {   

        map = this;
    }

 public void ResetAllNodes()
    {
        AddNodes();
        foreach (var item in Nodes)
            item.Reset();
    }

}
