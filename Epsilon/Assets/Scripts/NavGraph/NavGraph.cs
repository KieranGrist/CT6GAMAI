    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavGraph : MonoBehaviour
{
    //Pipeline : Create Node Tile Map ->  Generate Racetrack -> Generate Obstabcles -> Selector -> Generate Nav Mash - > Place AI units -> AI units select personality -> Place Player -> AI Unit Pathfinding -> Play!

    public List<Node> Nodes = new List<Node>();
    public static NavGraph map;
    private void Update()
    {
        map = this;
    }
    void Awake ()
    {
        map = this;
        DontDestroyOnLoad(gameObject.transform.parent);
    }
   public void GenerateNavMesh()
    {
        Nodes.Clear();
        Nodes.AddRange(FindObjectsOfType<Node>());
        for (int i = 0; i < Nodes.Count; i++)
            Nodes[i].Reset();
        Nodes.Clear();
        Nodes.AddRange(FindObjectsOfType<Node>());
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].Index = i;
            Nodes[i].GenerateNeighbours();
        }   
    }
}
