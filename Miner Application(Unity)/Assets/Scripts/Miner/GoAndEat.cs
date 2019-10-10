using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GoAndEat : State<Miner>
{
    public GoAndEat()
    {

    }
    public override void Execute(Miner agent)
    {
        agent.TargetLocation = new Vector3(2, 1, -2);
        Debug.Log("Eating my Pasty");
        agent.m_Hunger--;
    }
}
