﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanaryStateMachine 
{

    public State<Canary> PreviousState;
    public  Canary canary;
    public  Died DeathState;
    public  Fly FlyState;
    public Sing SingState;
    // Start is called before the first frame update

   public void Update()
    {
   
            CheckState();
  
    }

    public void ChangeState(State<Canary> newState)
    {
        PreviousState = null;
        PreviousState = canary.pState;

        canary.pState = null;
        canary.pState = newState;
    }

    public void CheckState()
    {
        CheckDeath();
        if (canary.pState == FlyState)
        {
            c_Fly();
        }
        if (canary.pState == DeathState)
        {
            Death();
        }
        if (canary.pState == SingState)
        {
            c_Sing();
        }
    }

    public bool CheckDeath()
    {

        if (canary.pState == DeathState)
        {
           
            canary.IsDead = true;
            ChangeState(DeathState);
            return true;
        }
        else
        {
            canary.c_MinerReference.FirstTime = true;
            canary.IsDead = false;
            return false;
        }
    }

    public void Death()
    {
        if (canary.c_Dead >= 5)
        {
            canary.c_Dead = 0;
            ChangeState(FlyState);
            
        }
    }

    public void c_Fly()
    {
 
        int random = Random.Range(0, 100);
        random = Random.Range(0, 100);
        random = Random.Range(0, 100);
        random = Random.Range(0, 100);
        bool StateChanged = false;
        if (random >= 90)
        {
            canary.IsDead = true;
            canary.c_MinerReference.m_Day = 0;
            Debug.Log( "The bird has died from poision gas" );
            ChangeState(DeathState);
            StateChanged = true;
        }
        if (!StateChanged)
        {
            if (canary.c_Flying > 10)
            {
                canary.IsDead = true;
               Debug.Log( "The bird is tired of flying and is now going to sing" );
                canary.c_Flying = 0;
                ChangeState(SingState);
            }
        }
    }

    public void c_Sing()
    {
       
        int random = Random.Range(0, 100);;
        random = Random.Range(0, 100);;
        random = Random.Range(0, 100);;
        random = Random.Range(0, 100);;
        bool StateChanged = false;
        if (random >= 90)
        {   
            canary.IsDead = true;
            canary.c_MinerReference.m_Day = 0;
           Debug.Log( "The bird has died from poision gas" );
            StateChanged = true;
            ChangeState(DeathState);
        
        }
        if (!StateChanged)
        {
            if (canary.c_Singing > 10)
            {
               Debug.Log( "The bird is tired of singing and wants to stretch its wings " );
                canary.c_Singing = 0;
                ChangeState(FlyState);
            }
        }
    }

}
