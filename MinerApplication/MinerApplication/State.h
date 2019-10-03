#pragma once

#include <iostream>
using namespace std;

//This is the BASE class for our states. We never use this as a state, as it should be purely an abstract class (made to be inheritted and it's methods overidden)
template <class T>
class State
{
public:
	State::State()
	{
	}


	State::~State()
	{
	}
	
	virtual void Execute(T* miner) = 0;
};


