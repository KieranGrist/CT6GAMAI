using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Edge> Neighbours = new List<Edge>(); //List of Neighbours 
    public int Index; //Index of the node
    Rigidbody rb; //Rigid Body Reference
    // Start is called before the first frame update
    public void GenerateNeighbours()
    {
        List<Node> hitObjects = new List<Node>();  //List of hit nodes
        name = "Node: " + Index; //Set the name of the node 
        foreach (var item in Physics.OverlapBox(transform.position, transform.localScale * 0.5f, transform.rotation)) //Loop through an overlap box 
        {
            Node Temp = item.gameObject.GetComponent<Node>(); //Create a temporary node and get the node object of a hit item 
            if (item.transform.gameObject != gameObject && Temp)  //If the item isnt this game object and temp exists
                hitObjects.Add(Temp); //Add temp to the list of hit nodes
        }
        foreach (var item in hitObjects) //Loop through the list of hit nodes
            Neighbours.Add(new Edge(this, item)); //Create and add a new neihbour between this and the hit node
        Destroy(gameObject.GetComponent<Rigidbody>()); //Destroy the rigidbody 

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale *2 );
    }
}
