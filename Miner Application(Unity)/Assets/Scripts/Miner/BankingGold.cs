using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankingGold : State<Miner>
{
    public BankingGold()
    {

    }
    public override void Execute(Miner agent)
    {
        agent.transform.position = new Vector3(0, 0, 0);
        agent.transform.position += new Vector3(0,1,0);
        //Print out information of gold
        Debug.Log("Banking Gold");
        //Banking the Miners gold
        agent.m_BankedGold += agent.m_Gold;
        agent.m_Gold = 0;;
    }
}
