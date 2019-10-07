using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sing : State<Canary>
{
    public Sing()
    {

    }
    public override void Execute(Canary agent)
    {
        agent.transform.position = new Vector3(0, -5, 6);
        agent.transform.position += new Vector3(0, 1, 0);
        Debug.Log("Tweet, tweet, tweet");
        agent.c_Singing++;
    }

}
