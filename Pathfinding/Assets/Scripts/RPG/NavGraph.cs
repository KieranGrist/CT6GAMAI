using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PathfinderType
{
    AStar,
    BreadthFirstSeach,
    DepthFirstSearch,
    Dijkstras,
    NoPath
}

public class NavGraph : MonoBehaviour
{
    public List<TileNode> Nodes = new List<TileNode>();
   public Pathfinder PathfindingTechnique;
    Pathfinder PreviousTechnique;
  public  PathfinderType pathfindingType;
    PathfinderType PreviousPathfinder;
    LayerMask TileMask;
    public bool ResetAllNodes;
    public static NavGraph map;

    void Awake()
    {
        InvokeRepeating("AddNodes", 0, 10);
        map = this;
      DontDestroyOnLoad(gameObject);

        PreviousPathfinder = PathfinderType.NoPath;
        TechniqueSelector();
     
    }


    void TechniqueSelector()
    {
        if (pathfindingType != PreviousPathfinder)
        {
            foreach (var item in gameObject.GetComponents<Pathfinder>())
               DestroyImmediate(item); 

            switch (pathfindingType)
            {
                case PathfinderType.AStar:
                    gameObject.AddComponent<ASTAR>();
                    break;
                case PathfinderType.BreadthFirstSeach:
                    gameObject.AddComponent<BFS>();
                    break;
                case PathfinderType.DepthFirstSearch:
                    gameObject.AddComponent<DFS>();
                    break;
                case PathfinderType.Dijkstras:
                    gameObject.AddComponent<Dijkstras>();
                    break;
            }
            PathfindingTechnique = gameObject.GetComponent<Pathfinder>();
            PreviousPathfinder = pathfindingType;
        }

    }
    void AddNodes()
    {
        Nodes.Clear();
        List<TileNode> TempNodesList = new List<TileNode>();
        TempNodesList.AddRange(FindObjectsOfType<TileNode>());
        foreach(var item in TempNodesList)      
                Nodes.Add(item);     
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i] == null)
                Nodes.Remove(Nodes[i]);
            else
            {
                Nodes[i].Index = i;             
                Nodes[i].GetComponent<TileNode>().enabled = true;
                Nodes[i].Reset();
            }
        }
    }
   
    // Update is called once per frame
    void Update()
    {   
        if (ResetAllNodes)
        {
            AddNodes();
            foreach (var item in FindObjectsOfType<TileNode>())
                item.Reset();

            ResetAllNodes = false;
        }
        map = this;
    }
}
