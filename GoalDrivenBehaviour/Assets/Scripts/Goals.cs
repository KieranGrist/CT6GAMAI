using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract classes contain no functionality, they are made to be inherited from
/// </summary>
public abstract class Goals
{
    public bool IsActive = false;

    public abstract void Activate();

    public abstract int Process();

    public abstract void Terminate();

    public abstract void AddSubgoal(Goals g);
}

/// <summary>
/// Represents a composite goal, this should be inherited from for custom functionality
/// </summary>
public class CompositeGoal : Goals
{
    //Stack represents a LIFO (last in, first out) container
    protected Stack<Goals> subgoals = new Stack<Goals>();

    /// <summary>
    /// We could use this for initialisations
    /// </summary>
    public override void Activate()
    {
        IsActive = true;
    }

    /// <summary>
    /// Adds a goal onto subgoals stack
    /// </summary>
    /// <param name="g"></param>
    public override void AddSubgoal(Goals g)
    {
        subgoals.Push(g);
        g.Activate();
    }

    /// <summary>
    /// Processes subgoals, also removes subgoals if applicable
    /// </summary>
    /// <returns></returns>
    public override int Process()
    {
        int status = subgoals.Peek().Process();

        if (status == -1) // if status is -1, it means we should terminate this goal
        {
            Goals g = subgoals.Pop();
            g.Terminate();
        }

        return status;
    }

    /// <summary>
    /// Any uninitialisations
    /// </summary>
    public override void Terminate()
    {
        IsActive = false;
    }
}

/// <summary>
/// Represents an atomic goal, this cannot have any subgoals
/// </summary>
public class AtomicGoal : Goals
{
    public override void Activate()
    {
        IsActive = true;
    }

    public override void AddSubgoal(Goals g)
    {
        //Does nothing
    }

    public override int Process()
    {
        int status = 1;

        //Perform custom logic here

        return status;
    }

    public override void Terminate()
    {
        IsActive = false;
    }
}

/// <summary>
/// Inherited from composite goals
/// </summary>
public class BuySword : CompositeGoal
{
    public override int Process()
    {
        int status = base.Process();

        //Here we could process some more custom logic to do with buying a sword
        if (NotEnoughGold())
        {
            subgoals.Push(new GetGold());
        }
        else
        {
           //BuySwords
        }

        return status;
    }

    bool NotEnoughGold()
    {
        //You should have a check here to actually check for gold, we're doing this to simply things
        return true;
    }
}

//Inherited from atomic goals
public class GetGold : AtomicGoal
{
    public override int Process()
    {
        int status = base.Process();

        //Perform custom getgold logic here, once we've got gold we set status to -1

        return status;
    }
}
