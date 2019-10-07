using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restock : State<ShopKeeper>
{
    public override void Execute(ShopKeeper agent)
    {
        agent.s_TimeTillNewStock--;
    }
}
