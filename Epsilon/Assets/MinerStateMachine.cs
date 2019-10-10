using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[System.Serializable]
public class MinerStateMachine
{
    public bool BirdChecked;
    public float Desierability;
    public State<Miner> PreviousState;
    public Miner miner;
    public Transition<Miner> m_Transition;
    public BankingGold BankingState;
    public MiningForGold MiningState;

    public void Start()
    {
        Func<int, int> square = x => x * x;
    }

    // Update is called once per frame
    public void Update()
    {

        CheckState();
    }


    public void CheckState()
    {
        miner.pState = Transition<Miner>.Transist(miner.pState, MiningState, BankingState, miner.m_Gold >= 10);
        miner.pState = Transition<Miner>.Transist(miner.pState, BankingState, MiningState, miner.m_Gold == 0);
    }
}