using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
/// <summary>
/// Helper class, this is used by the AIs to determine desier
/// </summary>
[System.Serializable]
 public class Helper
{

         /// <summary>
         /// Get the current distance from the agent to a target item
         /// </summary>
         /// <param name="Agent"></param>
         /// <param name="Target"></param>
         /// <param name="MaxDistance"></param>
         /// <returns></returns>
public     static float DistanceToItem(Transform Agent, Transform Target, float MaxDistance = 10)
    {
  
        float RET = Mathf.Clamp(Mathf.InverseLerp(0, MaxDistance, Vector3.Distance(Agent.position, Target.position)), 0, 1);       //Get distance between the two items, then inverse lerp that to be within the max distance then clamp it betweeen 0 and 1 
        return RET; //Return RET
    }
}
/// <summary>
/// Priority system, usses desire or any other float value to sort which tasks are the most desireable to the AI and then executes them
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class PQueue<T>
{
    /// <summary>
    /// List of keyvalue pairs used to be a task queue 
    /// </summary>
   public  List<KeyValuePair<float, State<T>>> TaskQueue = new List<KeyValuePair<float, State<T>>>();
    /// <summary>
    /// sort the list by the float/ desire
    /// </summary>
    public void Sort()
    {
       TaskQueue = TaskQueue.OrderBy(x => x.Key ).ToList(); //Order by the x.key then set it to be the current task queue list
    }
}