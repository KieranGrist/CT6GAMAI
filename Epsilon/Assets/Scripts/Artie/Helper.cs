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

         
    public static float DistanceToItem(Transform Agent, Transform Target, float MaxDistance = 10)
    {
        //INVERSE LERP TO KEEP IT IN MY RANGE OF 0 TO 1
        float RET = Mathf.Clamp(Mathf.InverseLerp(0, MaxDistance, Vector3.Distance(Agent.position, Target.position)), 0, 1);
        return RET;
    }
}
/// <summary>
/// Priority system, usses desire or any other float value to sort which tasks are the most desireable to the AI and then executes them
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class PQueue<T>
{
    public List<KeyValuePair<float, State<T>>> TaskQueue = new List<KeyValuePair<float, State<T>>>();
    public void Sort()
    {
       TaskQueue = TaskQueue.OrderBy(x => x.Key ).ToList();
    }
}