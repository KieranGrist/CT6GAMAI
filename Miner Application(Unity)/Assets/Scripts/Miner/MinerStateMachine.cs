using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct DesireDebug
{
    public float Status;
    public float DistanceToWayPoint;
    public float UnclappedDesire;
   
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
    public PriorityQueue<Miner> MinerQueue = new PriorityQueue<Miner>();
     public float K;
    //This is for debuging
 
    public DesireDebug goMineDesire;
    public DesireDebug goShopDesire, goEatDesire, goHomeDesire, goBirdDesire, goBankDesire, goDrinkDesire;
    public float MineDesire;
    public float ShopDesire;
    public float BankDesire;
    public float EatDesire;
    public float DrinkDesire;
    public float BirdDesire;
    public float HomeDesire;
    public float HighestDesire;
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
        return ((miner.m_Gold - 10) * -1) / 10;
        
    }
    public float ShopStatus ()
    {
        return (miner.m_BankedGold) / 10;
    }
    public float BankStatus()
    {
        return miner.m_Gold / 10;
    }
    public float BirdStatus()
    {
        return miner.m_Tiredness / 20;
    }
    public float DrinkStatus()
    {
        return miner.m_Thirstiness / 15;
    }
    public float EatStatus()
    {
        return miner.m_Hunger / 15;
    }
    public float HomeStatus()
    {
        return((miner.m_Tiredness - 20) * -1) / 20;
    }
    public void CheckDesire()
    {
        if (Helper.DistanceToItem(miner.transform, M_Transform) > 0)
        {
            goMineDesire.DistanceToWayPoint = Helper.DistanceToItem(miner.transform, M_Transform);
            goMineDesire.Status = MineStatus();
            goMineDesire.UnclappedDesire = K * (MineStatus() / Helper.DistanceToItem(miner.transform, M_Transform));
            MineDesire = Mathf.Clamp(K * (MineStatus() / Helper.DistanceToItem(miner.transform, M_Transform)), 0.0f, 1.0f);
        }
        if (Helper.DistanceToItem(miner.transform, S_Transform) > 0)
        {
            goShopDesire.Status = ShopStatus();
            goShopDesire.DistanceToWayPoint = Helper.DistanceToItem(miner.transform, S_Transform);
            goShopDesire.UnclappedDesire = K * (ShopStatus() / Helper.DistanceToItem(miner.transform, S_Transform));
            ShopDesire = Mathf.Clamp(K * (ShopStatus() / Helper.DistanceToItem(miner.transform, S_Transform)), 0.0f, 1.0f);
        }
        if (Helper.DistanceToItem(miner.transform, B_Transform) > 0)
        {
            goBankDesire.Status = BankStatus();
            goBankDesire.DistanceToWayPoint = Helper.DistanceToItem(miner.transform, B_Transform);
            goBankDesire.UnclappedDesire = K * (ShopStatus() / Helper.DistanceToItem(miner.transform, B_Transform));
            ShopDesire = Mathf.Clamp(K * (ShopStatus() / Helper.DistanceToItem(miner.transform, B_Transform)), 0.0f, 1.0f);
        }
        if (Helper.DistanceToItem(miner.transform, CB_Transform) > 0)
        {
            goBirdDesire.Status = BirdStatus();
            goBirdDesire.DistanceToWayPoint = Helper.DistanceToItem(miner.transform, CB_Transform);
            goBirdDesire.UnclappedDesire = K * (BirdStatus() / Helper.DistanceToItem(miner.transform, CB_Transform));
            ShopDesire = Mathf.Clamp(K * (BirdStatus() / Helper.DistanceToItem(miner.transform, CB_Transform)), 0.0f, 1.0f);
        }
        if (Helper.DistanceToItem(miner.transform, D_Transform) > 0)
        {
            goDrinkDesire.Status = DrinkStatus();
            goDrinkDesire.DistanceToWayPoint = Helper.DistanceToItem(miner.transform, D_Transform);
            goDrinkDesire.UnclappedDesire = K * (DrinkStatus() / Helper.DistanceToItem(miner.transform, D_Transform));
            ShopDesire = Mathf.Clamp(K * (DrinkStatus() / Helper.DistanceToItem(miner.transform, D_Transform)), 0.0f, 1.0f);
        }
        if (Helper.DistanceToItem(miner.transform, H_Transform) > 0)
        {
            goHomeDesire.Status = HomeStatus();
            goHomeDesire.DistanceToWayPoint = Helper.DistanceToItem(miner.transform, H_Transform);
            goHomeDesire.UnclappedDesire = K * (HomeStatus() / Helper.DistanceToItem(miner.transform, H_Transform));
            ShopDesire = Mathf.Clamp(K * (HomeStatus() / Helper.DistanceToItem(miner.transform, H_Transform)), 0.0f, 1.0f);
        }
        if (Helper.DistanceToItem(miner.transform, E_Transform) > 0)
        {
            goEatDesire.Status = EatStatus();
            goEatDesire.DistanceToWayPoint = Helper.DistanceToItem(miner.transform, E_Transform);
            goEatDesire.UnclappedDesire = K * (EatStatus() / Helper.DistanceToItem(miner.transform, E_Transform));
            ShopDesire = Mathf.Clamp(K * (EatStatus() / Helper.DistanceToItem(miner.transform, E_Transform)), 0.0f, 1.0f);
        }
    }
    public void CheckState()
    {

        MinerQueue.TaskQueue.Clear();
        CheckDesire();
        MinerQueue.TaskQueue.Add(new KeyValuePair<float, State<Miner>>(MineDesire, MiningState));
        MinerQueue.TaskQueue.Add(new KeyValuePair<float, State<Miner>>(ShopDesire, ShoppingState));
        MinerQueue.TaskQueue.Add(new KeyValuePair<float, State<Miner>>(BankDesire, BankingState));
        MinerQueue.TaskQueue.Add(new KeyValuePair<float, State<Miner>>(DrinkDesire, DrinkState));
        MinerQueue.TaskQueue.Add(new KeyValuePair<float, State<Miner>>(EatDesire, EatState));
        MinerQueue.TaskQueue.Add(new KeyValuePair<float, State<Miner>>(HomeDesire, HomeState));
        MinerQueue.TaskQueue.Add(new KeyValuePair<float, State<Miner>>(BirdDesire, CheckBirdState));

        MinerQueue.Sort();
        HighestDesire = MinerQueue.TaskQueue[0].Key;
        // miner.pState = MinerQueue.TaskQueue[0].Value;
        miner.pState = Transition<Miner>.Transist(miner.pState, MiningState, BankingState, miner.m_Gold >= miner.m_PickaxePower * 10);
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
        miner.pState = Transition<Miner>.Transist(miner.pState, HomeState, MiningState, miner.m_Tiredness <= 0);

    }

}
