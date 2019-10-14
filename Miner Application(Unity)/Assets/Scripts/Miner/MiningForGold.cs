using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MiningForGold : State<Miner>
{
    public MiningForGold()
    {

    }
    public override void Execute(Miner agent)
    {
        agent.m_CheckedCanary = false;
       // agent.transform.position = new Vector3(2, 1, 0);
       agent.TargetLocation = agent.m_StateMachine.M_Transform.position;
        //do
        //{

        //    agent.transform.position = Vector3.Lerp(agent.transform.position, new Vector3(2, 1, 0),Time.deltaTime);


        //} while (agent.transform.position != new Vector3(2, 1, 0));


        //Print out information on what it is doing...
    

        //Increment the miner's gold amount
        agent.MinerTimer += Time.deltaTime;
        if (agent.MinerTimer >= agent.Speed)
        {
            Debug.Log("Digging for gold!");
            agent.MinerTimer = 0;
            agent.m_Gold += agent.m_PickaxePower;
        }
        //Increment the miner's tiredness amount
        agent.m_Tiredness+= Time.deltaTime;
        //Increment the miner's thirstiness amount
        agent.m_Thirstiness+= Time.deltaTime;
        //Increment the miner's hunger amount
        agent.m_Hunger+= Time.deltaTime;
    }
}
