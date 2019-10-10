using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MinerStateMachine 
{
    public Miner miner;
    public BankingGold BankingState;
    public GoAndEat EatState;
    public GoAndHaveADrink DrinkState;
    public GoHomeAndSleep HomeState;
    public MiningForGold MiningState;
    public GoToTheShop ShoppingState;
    public GoAndCheckTheBird CheckBirdState;
    // Update is called once per frame
    public void Update()
    {
 
            CheckState();
    }
    public void CheckState()
    {
        float MineDesire, ShopDesire, BankDesire, EatDesire, DrinkDesire, BirdDesire, HomeDesire;
        
        /*
         * Mine Desire :
         * Distance to mine and how tired/hunger/thirsty they are 
         * 
         * Shop Desire:
         * Distance to shop and How much money they have
         * 
         * Bank Desire:
         * 
         * EatDesire:
         * 
         * DrinkDesire:
         * 
         * BirdDesire:
         * 
         * HomeDesire:
         * 
         */

        //Mining State Checks
        miner.pState = Transition<Miner>.Transist(miner.pState, MiningState, BankingState, miner.m_Gold >= miner.m_PickaxePower*10);
        miner.pState = Transition<Miner>.Transist(miner.pState, MiningState, DrinkState, miner.m_Thirstiness >= 15);
        miner.pState = Transition<Miner>.Transist(miner.pState, MiningState, EatState, miner.m_Hunger >= 13);
        miner.pState = Transition<Miner>.Transist(miner.pState, MiningState, CheckBirdState, miner.m_Tiredness >= 20);

        //Shopping State Checks
        miner.pState = Transition<Miner>.Transist(miner.pState, ShoppingState, MiningState, !miner.m_CanShop);

        //Banking State Checks 
        miner.pState = Transition<Miner>.Transist(miner.pState, BankingState, ShoppingState, miner.m_Gold == 0);
   
        //Hunger State Checks
        miner.pState = Transition<Miner>.Transist(miner.pState, EatState, MiningState, miner.m_Hunger <= 0);

        //Drink State Checks
        miner.pState = Transition<Miner>.Transist(miner.pState, DrinkState, MiningState, miner.m_Thirstiness <= 0);

        //Bird State Checks
        miner.pState = Transition<Miner>.Transist(miner.pState, CheckBirdState, HomeState, miner.m_Tiredness <= 0);

        //Home State Checks
        miner.pState = Transition<Miner>.Transist(miner.pState, HomeState, MiningState, miner.m_Tiredness <= 0 );
      
    }

}
