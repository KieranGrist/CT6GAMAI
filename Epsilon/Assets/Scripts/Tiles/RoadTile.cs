using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class which is used to represent roads 
/// </summary>
public  class RoadTile : Node
{
    private void Update()
    {
        gameObject.layer = 10; //Set layer to be 10
    }

    public override void Reset()
    {
        Vector3 Scale = new Vector3(); //Set scale to a blank vector 2
        Vector3 P = transform.position; //Set p to be the transform position 
        gameObject.AddComponent<Rigidbody>(); //add a rigid body to the node 
        Scale = new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f); //Set scale to be halve of the local scale 
        RB();


       var Walkable = true; //Set walkable to be true 
        if (Physics.BoxCast(transform.position - new Vector3(0, 20, 0), Scale, transform.up, out RaycastHit hit, transform.rotation, float.PositiveInfinity, LayerMask.GetMask("Road"))) //Boxcast up and if it hits a road 
            transform.rotation = hit.transform.GetComponent<Road>().transform.rotation; //Set the transform rotation to the item hit 
        else
            Walkable = false; //Set walkable to be false
        if (Physics.BoxCast(transform.position - new Vector3(0, 20, 0), Scale, transform.up, transform.rotation, float.PositiveInfinity, LayerMask.GetMask("Obstacle", "Walls"))) //boxcast up and if it hits a obstacle or wall 
            Walkable = false; //Sets walkable to be false
        Neighbours = new List<Edge>(); //Create a new list edge 
        Neighbours.Clear(); //clear the neighbours
        transform.position = P; //Set the position to be p 
        Destroy(GetComponent<Rigidbody>()); //destroy the rigid body 
        if (!Walkable) //If walkable is false 
            Destroy(this.gameObject); //Destroy the game object

    }

}
