using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BankingGold : State<Miner>
{
    public BankingGold()
    {

    }
    public override void Execute(Miner agent)
    {
        agent.TargetLocation = new Vector3(0, 1, 0);
        //Print out information of gold
        Debug.Log("Banking Gold");
        //Banking the Miners gold
        agent.m_BankedGold += agent.m_Gold;
        agent.m_Gold = 0;;
    }
}
