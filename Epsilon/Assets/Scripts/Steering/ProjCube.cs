using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
///<summary>
/// Class which handles the collision detection for steering behaviors like obstacle avoidence
/// </summary>
public class ProjCube : MonoBehaviour
{
    public List<GameObject> CollidedObjects; //List of collided objects
    private void OnTriggerEnter(Collider other)
    {

            CollidedObjects.Add(other.gameObject); //add collided object to list

    }
    private void OnTriggerExit(Collider other)
    {

            CollidedObjects.Remove(other.gameObject);  //Remove collided object to list
    }
    /// <summary>
    /// Returns the closet ai within the projection cube
    /// </summary>
    /// <param name="Other"></param>
    /// <returns></returns>
    public Vehicle CheckForAI(Vehicle Other)
    {
        var distance = float.MaxValue; //Set distance to be max
        Vehicle ret = null; //create a null return value
        foreach(var item in CollidedObjects)
        {
            var Ref = item.GetComponent<Vehicle>(); //Get a reference to the items vehicle componennt
            if (Ref) //If ref exists
            {
                var d = Vector3.Distance(Other.transform.position, item.transform.position); //Get distance between the vehicle and item

                if (d < distance) //If d is less then distance its closer 
                {
                    distance = d; //set distance to be d 
                    ret = Ref; //set return value to be Ref
                }
            }
        }
        return ret; //return ret 
    }

}
