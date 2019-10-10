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
       // agent.transform.position = new Vector3(2, 1, 0);
       agent.TargetLocation = new Vector3(2, 1, 0);
        //do
        //{

        //    agent.transform.position = Vector3.Lerp(agent.transform.position, new Vector3(2, 1, 0),Time.deltaTime);


        //} while (agent.transform.position != new Vector3(2, 1, 0));


        //Print out information on what it is doing...
        Debug.Log("Digging for gold!");

        //Increment the miner's gold amount
        agent.m_Gold+= 2;
    }
}
