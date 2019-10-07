using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : State<Canary>
{
    public Fly()
    {

    }
    public override void Execute(Canary agent)
    {
        agent.transform.position = new Vector3(2, -5, 6);
        agent.transform.position += new Vector3(0, 1, 0);
        Debug.Log("The Bird flys around the cage, energetically");
        agent.c_Flying++;
    }
}