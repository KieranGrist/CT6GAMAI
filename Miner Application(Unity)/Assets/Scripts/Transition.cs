using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Transition <t>
{
 
    public void Transist(State<t> From, State<t> To, bool Condition)
    {
        if (Condition == true)
        {
            
        }

    }
    public void ChangeState (State<t> newState)
    {

    }
    //public void ChangeState(State<t> CurrentState, State<t> NewState, out State<t> PreviousState)
    //{
    //    PreviousState = null;
    //    PreviousState = CurrentState;

    //    miner.pState = null;
    //    miner.pState = newState;
    //}
}
