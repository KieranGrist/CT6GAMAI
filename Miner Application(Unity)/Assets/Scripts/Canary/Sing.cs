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
        Debug.Log("Tweet, tweet, tweet");
        agent.c_Singing++;
    }

}
