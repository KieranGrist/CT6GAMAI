using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Fly : State<Canary>
{
    public Fly()
    {

    }
    public override void Execute(Canary agent)
    {
        agent.TargetLocation = new Vector3(2, -4, 6);
        Debug.Log("The Bird flys around the cage, energetically");
        agent.c_Flying++;
    }
}