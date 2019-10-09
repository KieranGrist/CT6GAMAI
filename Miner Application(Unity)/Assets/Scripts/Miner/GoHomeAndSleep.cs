using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GoHomeAndSleep : State<Miner>
{
    public GoHomeAndSleep()
    {

    }
    public override void Execute(Miner agent)
    {
        agent.transform.position = new Vector3(4, 0, 0);
        agent.transform.position += new Vector3(0, 1, 0);
        Debug.Log("I am at home and Sleeping");
        Debug.Log("Sleep ZZZ");
        agent.m_Tiredness--;
    }
}
