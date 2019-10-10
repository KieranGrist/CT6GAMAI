using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CanaryStateMachine
{
    public Canary canary;
    public Died DeathState;
    public Fly FlyState;
    public Sing SingState;
    // Start is called before the first frame update
    public void Update()
    {

        CheckState();

    }
    public void CheckState()
    {
        //Fly State Checks
        canary.pState = Transition<Canary>.Transist(canary.pState, FlyState, SingState, canary.c_Flying >= 10);
        canary.pState = Transition<Canary>.Transist(canary.pState, FlyState, DeathState, canary.c_Dead >= 0);

        //Sing State Checks
        canary.pState = Transition<Canary>.Transist(canary.pState, SingState, FlyState, canary.c_Singing >= 10);
        canary.pState = Transition<Canary>.Transist(canary.pState, SingState, DeathState, canary.c_Dead >= 0);

        //Death State Checks
        canary.pState = Transition<Canary>.Transist(canary.pState, DeathState, SingState, canary.c_Dead <= 0);
    }
}