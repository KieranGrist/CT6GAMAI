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
    public static NavGraph map;

    void Awake()
    {
        AddNodes();
        map = this;
      DontDestroyOnLoad(gameObject);

        PreviousPathfinder = PathfinderType.NoPath;
        TechniqueSelector();
     
    }


    void TechniqueSelector()
    {
        if (pathfindingType != PreviousPathfinder)
        {
            switch (pathfindingType)
            {
                case PathfinderType.AStar:
                    PathfindingTechnique = new ASTAR();
                    break;
                case PathfinderType.BreadthFirstSeach:
                    PathfindingTechnique = new BFS();
                    break;
                case PathfinderType.DepthFirstSearch:
                    PathfindingTechnique = new DFS();
                    break;
                case PathfinderType.Dijkstras:
                    PathfindingTechnique = new Dijkstras();
                    break;
            }         
            PreviousPathfinder = pathfindingType;
        }

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
