using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Transition<t>
{
    public static State<t> Transist(State<t> From, State<t> To, bool Condition, out bool RunFunction)
    {
       
        RunFunction = false;
        if (Condition == true)
        {
            RunFunction = true;
            return To;
        }
        else
        {
            RunFunction = false;    
            return From;
        }
    }
    public static State<t> Transist(State<t> CurrentState, State<t> From, State<t> To, bool Condition, out bool RunFun)
    {
        RunFun = false;
        if (Condition == true)
        {
            return To;
        }
        else
        {
            if (CurrentState == From)
                return From;
            else
                return CurrentState;
        }
    }
    public static State<t> Transist(State<t> CurrentState, State<t> From, State<t> To, bool Condition)
    {
        if (Condition == true)
        {
            return To;
        }
        else
        {
            if (CurrentState == From)
                return From;
            else
                return CurrentState;
        }

    }
}