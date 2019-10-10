using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Restock : State<ShopKeeper>
{
    public override void Execute(ShopKeeper agent)
    {
        Debug.Log("Finding a new pickaxe");
        agent.TargetLocation = new Vector3(9, -4, 2);
        agent.s_TimeTillNewStock--;
    }
}
