using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Died : State<Canary>
{
    public Died()
    {

    }
    public override void Execute(Canary agent)
    {
        agent.transform.position = new Vector3(4, -5, 6);
        agent.transform.position += new Vector3(0, 1, 0);
        agent.c_Dead = agent.c_MinerReference.m_Day;
    }
}
