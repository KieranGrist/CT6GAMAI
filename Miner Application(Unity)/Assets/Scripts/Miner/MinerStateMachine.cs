using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct GoMineDesire
{
    public float GoldDesire;
    public float DistanceToMine;
    public float UnclappedMineDesire;
};
[System.Serializable]

public struct GoShopDesire
{
    public float ShopDesire;
    public float DistanceToShop;
    public float UnclappedShopDesire;
}
[System.Serializable]
public class MinerStateMachine 
{
    public Transform M_Transform;
    public Transform B_Transform;
    public Transform E_Transform;
    public Transform D_Transform;
    public Transform H_Transform;
    public Transform S_Transform;
    public Transform CB_Transform;
    public Miner miner;
    public PriorityQueue<Miner> MinerQueue;
     public float K;
    //This is for debuging
 
    public GoMineDesire goMineDesire;
    public GoShopDesire goShopDesire;
     float MineDesire;
     float ShopDesire;
     float BankDesire;
     float EatDesire;
     float DrinkDesire;
     float BirdDesire;
     float HomeDesire;

    public GoAndCheckTheBird CheckBirdState;
    public BankingGold BankingState;
    public GoAndEat EatState;
    public GoAndHaveADrink DrinkState;
    public GoHomeAndSleep HomeState;
    public MiningForGold MiningState;
    public GoToTheShop ShoppingState;
    // Update is called once per frame
    public void Update()
    {
 
            CheckState();
    }
    public float MineStatus()
    {
        // This will take in an agent and return a number between 0 and 1
        // 1.0 = Need to mine
        // 0 = No need to mine
        float RET = ((miner.m_Gold - 10) * -1) / 10;
        return RET;
    }
    public float ShopStatus ()
    {
        return (miner.m_BankedGold) / 10;
    }
    public void CheckState()
    {

    
        /*
         * Mine Desire :
         * Distance to mine and how tired/hunger/thirsty they are 
         * 
         * Shop Desire:
         * Distance to shop and How much money they have
         * 
         * Bank Desire:
         * Distance to bank and how much money you have to bank
         * 
         * EatDesire:
         * Distance to Home and how hungry you are  
         * DrinkDesire:
         * Distance to Home and how thirsty you are 
         * BirdDesire:
         * Distance to bird and time since the last check
         * HomeDesire:
         * Distance to home and how tired you are
         */

        if (Helper.DistanceToItem(miner.transform, M_Transform) > 0)
        {
            goMineDesire.DistanceToMine = Helper.DistanceToItem(miner.transform, M_Transform);
            goMineDesire.GoldDesire =  MineStatus();
            goMineDesire.UnclappedMineDesire = K * (MineStatus() / Helper.DistanceToItem(miner.transform, M_Transform));
            MineDesire = Mathf.Clamp(K * ( MineStatus() / Helper.DistanceToItem(miner.transform, M_Transform)), 0, 1);
        }
        if (Helper.DistanceToItem(miner.transform, S_Transform) > 0)
        {
            ShopDesire = Mathf.Clamp(K * (1 - ShopStatus() / Helper.DistanceToItem(miner.transform, S_Transform)), 0, 1);
        }
        BankDesire = K * (1 );
        BirdDesire = K * (1);
        DrinkDesire = K * (1);
        EatDesire = K * (1);
        HomeDesire = K * (1);
        ShopDesire = K * (1);
        // *bubble sort arrays 
       
        // Priority quee onto that que, that is going to have a map of data, value represting the desirability, other value is the state. what it does is it will evaulate a thing, get a score for the desire, here is the state for that, add that into a pair, add that to the priority que
        // the Priority quee keep the highest number ontop and do the highest state first. 

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
