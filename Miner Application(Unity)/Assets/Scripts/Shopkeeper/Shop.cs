using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Shop : State<ShopKeeper>
{
    public override void Execute(ShopKeeper agent)
    {
        agent.TargetLocation = new Vector3(9, -4, -2);
        agent.s_TimeTillNewStock++;
        agent.s_Tiredness++;
        Debug.Log("Manning the shop *whistles*");
   
 
         
            if (agent.s_PickaxePurchased == true)
            {
                Debug.Log("Pickaxe purchased, thankyou for your custom");
                agent.s_Cost *= 2;
                agent.s_TimeTillNewStock += 10;
                agent.s_PickaxePurchased = false;
            }
        

    }
}
