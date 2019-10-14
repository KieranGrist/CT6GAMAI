using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{

    public static float DistanceToItem(Transform Agent, Transform Target)
    {
        return Mathf.Clamp(Vector3.Distance(Target.position, Agent.position),0,1);
    }
}
