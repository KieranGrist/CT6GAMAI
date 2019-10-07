using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : State<ShopKeeper>
{
    public override void Execute(ShopKeeper agent)
    {
        agent.s_TimeTillNewStock++;
        
    }
    public void Purchase(Miner miner)
    {

    }
}
