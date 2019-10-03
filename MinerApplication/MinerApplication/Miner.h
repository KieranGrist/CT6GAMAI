#pragma once


//Forward declaring state to avoid issues with circular dependency
class State;

class Miner
{
public:
	Miner();
	~Miner();
	
	//Our state
	State* pState;

	//This is called by our console application periodically
	void Update();

	//Use this method to change states, so the old state is correctly disposed of
	void ChangeState(State *newState);

	//public members
	//These values can be monitored and editted by our "states"
	int m_Gold;
	int m_BankedGold;

};

