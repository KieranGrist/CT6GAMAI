using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GoToTheShop : State<Miner>
{
    public override void Execute(Miner agent)
    {
        agent.TargetLocation = new Vector3(10, -4, -2);
        Debug.Log("I am going to visit the shop today");
        if (agent.m_BankedGold >= agent.m_ShopKeeeperReference.s_Cost)
        {
            Debug.Log("I have enough money");

            if (agent.m_ShopKeeeperReference.pState == agent.m_ShopKeeeperReference.s_StateMachine.ShopState)
            {
                Debug.Log("I would like to buy a better pickaxe please");
                agent.m_BankedGold -= agent.m_ShopKeeeperReference.s_Cost;
                agent.m_ShopKeeeperReference.s_Gold += agent.m_ShopKeeeperReference.s_Cost;
                agent.m_ShopKeeeperReference.s_PickaxePurchased = true;
                agent.m_PickaxePower *= 2;
            }
            else
            {
                Debug.Log("The shop is not open right now");
            }
        }
        else
        {
            Debug.Log("I do not have enough money");
        }
        agent.m_CanShop = false;
    }
}