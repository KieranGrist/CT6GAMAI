using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Fly : State<Canary>
{
    public Fly()
    {

    }
    public override void Execute(Canary agent)
    {
        agent.TargetLocation = new Vector3(2, -4, 6);
        //Debug.Log("The Bird flys around the cage, energetically");
        agent.c_Flying+=Time.deltaTime;
        if (agent.c_Singing >=0)
            agent.c_Singing -= Time.deltaTime;

        int Rand = Random.Range(0, 100);
        if (Rand >= 99)
        {
            agent.c_Dead = 60;
        }
    }
}