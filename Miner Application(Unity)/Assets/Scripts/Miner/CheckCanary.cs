using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CheckCanary : State<Miner>
{
    public CheckCanary()
    {

    }
    public override void Execute(Miner agent)
    {
        agent.transform.position = new Vector3(4, 0, -2);
        agent.transform.position += new Vector3(0, 1, 0);
        agent.m_CheckedCanary = true;
        agent.m_CanaryReference.c_StateMachine.CheckDeath();
        Debug.Log("I am checking the canary");
        if (agent.m_CanaryReference.IsDead == true)
        {
            if (agent.FirstTime)
            {
                agent.m_Day = 0;
                agent.FirstTime = false;
            }
            Debug.Log("*ALARM* *ALARM* THE BIRD HAS DIED, MINE WILL CLOSE FOR 5 DAYS This is Day" + agent.m_CanaryReference.c_Dead);
        }
        else
        {
            Debug.Log("The canary is alive and well");
        }
    }
}
