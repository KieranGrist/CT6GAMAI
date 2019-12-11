using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Generated nodes in a set area 
/// </summary>
public class NodeGenerator : MonoBehaviour
{
    [Header("Tile Map Settings")]
    public static NodeGenerator MapGen; //Static reference of the script 
    public float GapBetweenNodes = 1; //Gap between the nodes 
    public int Area; //Cubic area of the nodes
    public GameObject Cube; //Node cube to spawn in 

    List<GameObject> TempNodes = new List<GameObject>(); //List of node game objects
    private void Awake()
    {
        MapGen = this; //Set reference to be this
    }
    /// <summary>
    /// Creates a new grid of nodes
    /// </summary>
    public void GenerateMap()
    {
        float t = transform.position.x + (Area * GapBetweenNodes); //Set t to be position x added by area * gapbetween nodes
        t *= 0.5f; //Halve the t value
        for (float x = transform.position.x - t; x < (transform.position.x + (Area * GapBetweenNodes)) * 0.5f; x += GapBetweenNodes) //Loop for the x values of the grid, the grid starts halve way so that the starting position is within the middle of the grid 
            for (float z = transform.position.z - t; z < (transform.position.z + (Area * GapBetweenNodes)) * 0.5f; z += GapBetweenNodes) //Loop for the z values of the grid, the grid starts halve way so that the starting position is within the middle of the grid
            {
                GameObject go = Instantiate(Cube, new Vector3(x, 0, z), transform.rotation, transform);  //Create a new node 
                go.transform.localScale = new Vector3(GapBetweenNodes, 0.01f, GapBetweenNodes); //Set the scale of the node 
                TempNodes.Add(go); //Add it to the list
            }
    }

    public void GenerateMapForTrack()
    {
        foreach (var item in TempNodes)
        {
            Destroy(item);
        }
        var OldGapBetweeenNodes = GapBetweenNodes;
        GapBetweenNodes = 8;
        var OldArea = Area;
        Area = 200;
        DateTime StartTime = DateTime.Now;
        float t = transform.position.x + (Area * GapBetweenNodes);
        t *= 0.5f;
        for (float x = transform.position.x - t; x < (transform.position.x + (Area * GapBetweenNodes)) * 0.5f; x += GapBetweenNodes)
            for (float z = transform.position.z - t; z < (transform.position.z + (Area * GapBetweenNodes)) * 0.5f; z += GapBetweenNodes)
            {
                GameObject go = Instantiate(Cube, new Vector3(x, 0, z), transform.rotation, transform);
                go.transform.localScale = new Vector3(GapBetweenNodes, 0.01f, GapBetweenNodes);
                TimeCalculated = DateTime.Now - StartTime;
                FunctionTime = (float)TimeCalculated.TotalSeconds;
                
                TempNodes.Add(go);
                go.AddComponent<ProcdualNode>();

            }
        GapBetweenNodes = OldGapBetweeenNodes;
        Area = OldArea;
        NavGraph.map.GenerateTrackNavMesh();

    }
    }
    /// <summary>
    /// Add selectors to the nodes
    /// </summary>
    public void AddSelector()
    {
        foreach (var item in TempNodes) //Loop through all the nodes created
            item.AddComponent<Selector>(); //Add a selector to them 
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.position.x + (Area * GapBetweenNodes), 0.01f, transform.position.z + (Area * GapBetweenNodes))); //Draw a wire cube representating the area the node will be generated in
    }

}