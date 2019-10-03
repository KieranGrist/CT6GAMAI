#pragma once

template <class T>
class State;
class Miner;
class Canary
{
public:
	Canary();
	~Canary();

	//Our state
	State<Canary>* pState;

	//This is called by our console application periodically
	void Update();

	//Use this method to change states, so the old state is correctly disposed of
	void ChangeState(State<Canary>* newState);

	//public members
	//These values can be monitored and editted by our "states"
	int c_Signing;
	int c_Flying;
	bool c_Dead;
	Miner* c_MinerReference;
};

