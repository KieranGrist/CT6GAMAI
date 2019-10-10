using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAndCheckTheBird : State<Miner>
{
    public override void Execute(Miner agent)
    {
        agent.TargetLocation = new Vector3(4, -4, 7);
        if (!agent.Moving)
        {
            agent.MinerTimer += Time.deltaTime;
            if (agent.MinerTimer >= 15)
            {
                agent.MinerTimer = 0;

                if (agent.m_CanaryReference.c_Dead >= 0)
                {
                    Debug.Log("ALERT, ALERT, THE BIRD HAS BEEN FOUND DEAD");
                    Debug.Log("I will now have to clear out the gas ");
                    
                  
                }
                else
                {
                    Debug.Log("The Bird is happy and well and I can now leave the cave");
                     agent.m_CheckedCanary = true;
                }
                
               
            }

        }
    }
}