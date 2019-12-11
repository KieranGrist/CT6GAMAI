    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Navigation Graph, stores all the nodes 
/// </summary>
public class NavGraph : MonoBehaviour
{
    public List<Node> Nodes = new List<Node>(); //List of the nodes
    public static NavGraph map; //Static reference towards the map
    private void Update()
    {
        map = this; //set reference to be this
    }
    void Awake ()
    { 
        map = this; //set reference to be this 
    }
      public void GenerateTrackNavMesh()
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
            Nodes[i].GenerateNeighboursDefault();
        }
    }
    /// <summary>
    /// Searched for nodes and adds them to the list
    /// Also resets the nodes and ensures their neihbours have been set
    /// </summary>
    public void GenerateNavMesh()
    {
        Nodes.Clear(); //Clear nodes
        Nodes.AddRange(FindObjectsOfType<Node>()); //Find all nodes in the game world and add them to this nav graph
        for (int i = 0; i < Nodes.Count; i++) //Loop through each ndoe 
            Nodes[i].Reset(); //Reset the node
        Nodes.Clear(); //Clear the list as some reset functions can delete the nodes
        Nodes.AddRange(FindObjectsOfType<Node>());  //Find all nodes in the game world and add them to this nav graph
        for (int i = 0; i < Nodes.Count; i++) //Loop through each ndoe 
        {
            Nodes[i].Index = i; //Set indx to be i 
            Nodes[i].GenerateNeighbours(); //Generate the Neighbours of the node
        }   
    }
}
