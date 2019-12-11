using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Types of roads there can be 
/// </summary>
public  enum RoadTypes
{
    Blank,
    Road
}
[ExecuteInEditMode]
public class Selector : MonoBehaviour
{
     RoadTypes type = RoadTypes.Blank; //Road type
    // Start is called before the first frame update
    void Awake()
    {
        RoadSelector(); //Call Road selector function
    }
    /// <summary>
    /// This should only run once the procedural generation has finished
    /// Only needs to be ran once!
    /// </summary>
    void RoadSelector()
    {
        type = RoadTypes.Blank; //Set road type to be black
        bool Dest = false;// Stores if the object should be destroyed or not
        foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f), transform.rotation, LayerMask.GetMask("Walls"))) //Overlaps a box and checks if there is a wall within this box
            Dest = true; //Set dest to true
        foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f), transform.rotation, LayerMask.GetMask("Obstacle"))) //Overlaps a box and checks if there is a Obstacle within this box

            Dest = true; //Set dest to true

        if (!Dest) //If Dest is falsse
            foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f), transform.rotation, LayerMask.GetMask("Road"))) //Overlap a box
            {
                if (!GetComponent<RoadTile>()) //If selector doesnt have a road tile 
                    gameObject.AddComponent<RoadTile>(); //add a road tile 
                if (item.GetComponent<Straight>()) //If it is a Straight
                    type = RoadTypes.Road; //Set type to be road
                if (item.GetComponent<Corner>()) //If it is a Corner
                    type = RoadTypes.Road; //Set type to be road
            }
        else
            Destroy(gameObject); //Destroy the game object
        if (type == RoadTypes.Blank) //If blank 
            Destroy(gameObject); //Destroy the game object
    }
}
