using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAndEat : State<Miner>
{
    public GoAndEat()
    {

    }
    public override void Execute(Miner agent)
    {
        agent.transform.position = new Vector3(2, 0, -2);
        agent.transform.position += new Vector3(0, 1, 0);
        Debug.Log("Eating my Pasty");
        agent.m_Hunger--;
    }
}
