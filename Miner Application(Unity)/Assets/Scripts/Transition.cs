using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Transition<t>
{
    public static State<t> Transist(State<t> From, State<t> To, bool Condition, out bool RunFunction) //HIGHLY RECOMENDED YOU DO NOT USE THIS FUNCTION, IT CAN BE VERY DESTRUCTIVE. 
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
            if (CurrentState == From)
                return To;
            else
                return CurrentState;
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
            if (CurrentState == From)
                return To;
            else
                return CurrentState;
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