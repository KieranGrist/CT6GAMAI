using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Died : State<Canary>
{
    public Died()
    {

    }
    public override void Execute(Canary agent)
    {
        agent.TargetLocation = new Vector3(4, -4, 6);
        agent.c_Dead = agent.c_MinerReference.m_Day;
    }
}
