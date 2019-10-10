using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sleep : State<ShopKeeper>
{
    public override void Execute(ShopKeeper agent)
    {
        agent.TargetLocation = new Vector3(9, -4, 0);
        Debug.Log("SLEEP ZZZZ");
        agent.s_Tiredness--;
    }
}
