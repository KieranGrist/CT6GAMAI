using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum PathfinderType
{
    AStar,
    BreadthFirstSeach,
    DepthFirstSearch,
    Dijkstras,
}
[System.Serializable]
[ExecuteInEditMode]
public class NavGraph : MonoBehaviour
{
    public List<TileNode> Nodes = new List<TileNode>();
    public TileMaterials MaterialManager;
   public Pathfinder PathfindingTechnique;
    Pathfinder PreviousTechnique;
  public  PathfinderType pathfindingType;
    PathfinderType PreviousPathfinder;
    LayerMask TileMask;
    public bool ResetAllNodes;



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
            gameObject.GetComponent<Pathfinder>().Map = GetComponent<NavGraph>();
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
                Nodes[i].MaterialManager = MaterialManager;
                Nodes[i].GetComponent<TileNode>().enabled = true;
                Nodes[i].Reset();
            }
        }
    }
    void Start()
    {
        TechniqueSelector();
        InvokeRepeating("AddNodes", 0, 10); 
    }
    // Update is called once per frame
    void LateUpdate()
    {   
 
        TechniqueSelector();  
        if (ResetAllNodes)
        {
            AddNodes();
            foreach (var item in FindObjectsOfType<TileNode>())
                item.Reset();

            ResetAllNodes = false;
        }    
    }
}
