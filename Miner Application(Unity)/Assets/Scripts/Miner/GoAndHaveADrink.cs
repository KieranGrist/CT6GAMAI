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
        agent.transform.position = new Vector3(0, 0, -2);
        agent.transform.position += new Vector3(0, 1, 0);
        Debug.Log("Having A Drink ");
        agent.m_Thirstiness--;
    }
}
