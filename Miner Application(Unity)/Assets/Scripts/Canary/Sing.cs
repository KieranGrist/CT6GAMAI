using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sing : State<Canary>
{
    public Sing()
    {

    }
    public override void Execute(Canary agent)
    {
        agent.TargetLocation = new Vector3(0, -4, 6);
        //Debug.Log("Tweet, tweet, tweet");
        agent.c_Singing+=Time.deltaTime;
        if (agent.c_Flying >= 0)
            agent.c_Flying -= Time.deltaTime;
        int Rand = Random.Range(0, 1000);
        if (Rand >= 999)
        {
            agent.c_Dead = 60;
        }
    }

}
