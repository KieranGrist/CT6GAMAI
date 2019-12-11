using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Abstract function which can be used to create a state that is executed when it is desired
/// </summary>
/// <typeparam name="t"></typeparam>
 public abstract class State <t>
{
    /// <summary>
    /// Execute this state function
    /// </summary>
    /// <param name="agent"></param>
    public abstract void Execute(t agent);
}
