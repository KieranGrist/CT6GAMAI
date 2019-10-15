using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[System.Serializable]
public class Helper
{

         
    public static float DistanceToItem(Transform Agent, Transform Target, float MaxDistance = 10)
    {
        //INVERSE LERP TO KEEP IT IN MY RANGE OF 0 TO 1

        return Mathf.Clamp(Mathf.InverseLerp(0, MaxDistance, Vector3.Distance( Agent.position,Target.position)),0, 1);
    }
}
[System.Serializable]
public class PriorityQueue<T>
{
    public List<KeyValuePair<float, State<T>>> TaskQueue = new List<KeyValuePair<float, State<T>>>();
    public void Sort()
    {
        //https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-3.php

        TaskQueue = TaskQueue.OrderBy(x => x.Key ).ToList();
    }
}