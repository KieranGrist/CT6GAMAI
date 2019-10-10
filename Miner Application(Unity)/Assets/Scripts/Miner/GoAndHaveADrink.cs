using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GoAndHaveADrink : State<Miner>
{
    public GoAndHaveADrink()
    {

    }
    public override void Execute(Miner agent)
    {
        agent.TargetLocation = new Vector3(0, 1, -2);
        Debug.Log("Having A Drink ");
        agent.m_Thirstiness-= Time.deltaTime;
    }
}
