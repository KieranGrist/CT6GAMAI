using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MinerStateMachine 
{
    public bool BirdChecked;
    public State<Miner> PreviousState;
    public Miner miner;
    public BankingGold BankingState;
    public CheckCanary CanaryState;
    public GoAndEat EatState;
    public GoAndHaveADrink DrinkState;
    public GoHomeAndSleep HomeState;
    public MiningForGold MiningState;
    public GoToTheShop ShoppingState;
    // Start is called before the first frame update
    public void Start()
    {
       
    }

    // Update is called once per frame
    public void Update()
    {
 
            CheckState();
    }
    public void ChangeState(State<Miner> newState)
    {
        PreviousState = null;
        PreviousState = miner.pState;

        miner.pState = null;
        miner.pState = newState;
    }

    public void CheckState()
    {
        if (miner.pState == BankingState)
        {
            Bank();
        }
        if (miner.pState == EatState)
        {
            Eat();
        }
        if (miner.pState == DrinkState)
        {
            Drink();
        }
        if (miner.pState == HomeState)
        {
            Home();
        }
        if (miner.pState == MiningState)
        {
            Mining();
        }
        if (miner.pState == CanaryState)
        {
            m_Canary();
        }
        if (miner.pState == ShoppingState)
        {
            GoToShop();
        }
    }
    public void GoToShop ()
    {
        if (!miner.m_CanShop)
        {
            Debug.Log("I have visited the shop and will continue to mine");
            ChangeState(MiningState);
        }
    }

    public void m_Canary()
    {
        if (miner.m_CheckedCanary)
        {
            ChangeState(PreviousState);
            miner.m_CheckedCanary = false;
        }
    }

    public void Bank()
    {
        if (miner.m_Gold == 0)
        {
            Debug.Log("I have banked all the gold, I now have " + miner.m_BankedGold + " Stored in the bank account I will carry on mining");
            ChangeState(MiningState);
            miner.m_CanShop = true;
     
        }
    }

    public void Eat()
    {
        if (miner.m_Hunger <= 0)
        {
            Debug.Log( "I have eaten and will now return" );
            ChangeState(PreviousState);
            miner.m_Hunger = 0;
        }
    }

    public void Drink()
    {
        if (miner.m_Thirstiness <= 0)
        {
            Debug.Log( "I have drinken and will now return" );
            ChangeState(PreviousState);
            miner.m_Thirstiness = 0;
        }
    }

    public void Home()
    {
        bool ChangedState = false;
        if (BirdChecked == false)
        {
            Debug.Log( "The day has now ended, as such, you are sent to check the canary" );
            ChangeState(CanaryState);
            BirdChecked = true;
            Debug.Log( "The Bird Has been checked " );
        }
        else
        {


            if (miner.m_Hunger > 0)
            {
                Debug.Log( "I need food, i will now go and eat" );
                ChangeState(EatState);
                ChangedState = true;
            }
            if (!ChangedState)
            {
                if (miner.m_Thirstiness > 0)
                {
                    Debug.Log( "I need drink, i will now go and drink" );
                    ChangeState(DrinkState);
                }
            }
            if (!ChangedState)
            {
                if (miner.m_Tiredness <= 0)
                {
                    miner.m_Day++;

                    if (miner.m_CanaryReference.c_StateMachine.CheckDeath())
                    {
                        Debug.Log( "Mine is still closed, I will rest for another day" );
                        ChangeState(HomeState);
                    }
                    else
                    {
                        Debug.Log( "I am at now ready for a new days work, I will now head to the mine" );
                        ChangeState(MiningState);
                    }

                    miner.m_Tiredness = 0;
                    BirdChecked = false;
                }
            }
        }
    }

    public void Mining()
    {
        bool ChangedStates = false;
        if (miner.m_CanShop)
        {
            ChangeState(ShoppingState);
            ChangedStates = true;
        }
        if (!ChangedStates)
        {
            if (miner.m_Gold >= 10)
            {
                ChangeState(BankingState);
                Debug.Log( "I have no more room for my gold, I must go to the bank" );
                ChangedStates = true;
            }
        }
        if (!ChangedStates)
        {
            if (miner.m_Tiredness >= 20)
            {
                ChangeState(HomeState);
                Debug.Log( "I am tired I now need to go home and sleep" );
                ChangedStates = true;
            }
        }
        if (!ChangedStates)
        {
            if (miner.m_Thirstiness >= 15)
            {
                ChangeState(DrinkState);
                Debug.Log( "I am thirsty I must go and have a drink" );
                ChangedStates = true;
            }
        }
        if (!ChangedStates)
        {
            if (miner.m_Hunger >= 13)
            {
                ChangeState(EatState);
                ChangedStates = true;
            }
        }
    }



}
