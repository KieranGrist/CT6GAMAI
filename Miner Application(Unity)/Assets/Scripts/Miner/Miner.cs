using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public Canary m_CanaryReference;
    public MinerStateMachine m_StateMachine;
    public int m_Day = 0;
    public int m_PickaxePower = 0;
    public int m_Gold = 0;
    public int m_BankedGold = 0;
    public int m_Tiredness = 0;
    public int m_Thirstiness = 0;
    public int m_Hunger = 0;
    public bool m_CheckedCanary = false;
    public bool FirstTime = true;
    public State<Miner> pState;
    public float SleepTimer =0;
    public Transform m_TransformReference;
    void Start()
    {
        m_StateMachine.BankingState = new BankingGold();
        m_StateMachine.CanaryState = new CheckCanary();
        m_StateMachine.EatState = new GoAndEat();
        m_StateMachine.DrinkState = new GoAndHaveADrink();
        m_StateMachine.HomeState = new GoHomeAndSleep();
        m_StateMachine.MiningState = new MiningForGold();
        pState = m_StateMachine.MiningState;

    }

    // Update is called once per frame
    void Update()
    {
        m_TransformReference = this.transform;
        SleepTimer += Time.deltaTime;
        if (SleepTimer >= 2)
        {
            m_StateMachine.miner = this;
            m_StateMachine.Update();
            pState.Execute(this);
            SleepTimer = 0;
        }
    }
}
