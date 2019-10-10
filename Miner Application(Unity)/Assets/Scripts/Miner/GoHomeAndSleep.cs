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
        
        agent.TargetLocation = new Vector3(4, 1, 0);
  
        Debug.Log("I am at home and Sleeping");
        Debug.Log("Sleep ZZZ");
        agent.m_Tiredness--;
    }
}
