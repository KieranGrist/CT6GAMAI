#pragma once
#include "MinerStateMachine.h"
//Forward declaring state to avoid issues with circular dependency
template <class T>
class State;
class Canary;
class Miner
{
public:
	Miner();
	~Miner();
	MinerStateMachine m_StateMachine;
	//Our state
	State<Miner>* pState;
	void Start();

	//This is called by our console application periodically
	void Update();

	//Use this method to change states, so the old state is correctly disposed of
	

	//public members
	//These values can be monitored and editted by our "states"
	int m_Gold = 0;
	int m_BankedGold = 0;
	int m_Tiredness = 0;
	int m_Thirstiness = 0;
	int m_Hunger = 0;
	bool m_CanHearCanary = 0;
	Canary* m_CanaryReference;
};

