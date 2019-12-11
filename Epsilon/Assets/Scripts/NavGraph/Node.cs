using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public abstract class Node : MonoBehaviour
{
    public List<Edge> Neighbours = new List<Edge>(); //List of neghbours 
     float Cost = float.PositiveInfinity; //set the cost of the node to be infinity 
    public int Index; //Index of the node
    Rigidbody rb; //Rigid Body Reference
    // Start is called before the first frame update
    /// <summary>
    /// Rigid Body Function which ensures the rigid body is static and wont move 
    /// </summary>
    public void RB()
    {
        rb = GetComponent<Rigidbody>(); //Get the rigid body attached to the game object
        rb.useGravity = false; // Set gravity to be false
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY; //Stop it from moving
        rb.freezeRotation = true; //Freeze its rotation
        gameObject.layer = 10;  //Set the layer of the game object to be 10
    }
    /// <summary>
    /// Looks for nodes around it to be neighbours with
    /// </summary>
    public void GenerateNeighbours()
    {
        List<Node> hitObjects = new List<Node>();  //List of hit nodes
        name = "Node: " + Index; //Set the name of the node 
        foreach (var item in Physics.OverlapBox(transform.position , transform.localScale * 0.5f, transform.rotation)) //Loop through an overlap box 
        {
            Node Temp = item.gameObject.GetComponent<Node>(); //Create a temporary node and get the node object of a hit item 
            if (item.transform.gameObject != gameObject && Temp )  //If the item isnt this game object and temp exists
                    hitObjects.Add(Temp); //Add temp to the list of hit nodes
        }
        foreach (var item in hitObjects) //Loop through the list of hit nodes
            Neighbours.Add(new Edge(this, item)); //Create and add a new neihbour between this and the hit node
        Destroy(gameObject.GetComponent<Rigidbody>()); //Destroy the rigidbody 

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black; //Set colour to be black 
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x, 1, transform.localScale.z)); //Draw a wire cube representing the collision checker


        Gizmos.color = Color.red;//Set the colour to be black 
        var Matrix = Gizmos.matrix; //Stores old matrix
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward , transform.rotation, transform.localScale * 2); //Set the gizmo matrix to be that of my transforms matrix

        Gizmos.DrawWireCube(Vector3.zero, Vector3.one); //Draw the cube with the given matrix 
        Gizmos.matrix = Matrix; //Set the gizmo matrix back to the old one 
    }
    public abstract void Reset();
}
