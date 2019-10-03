#pragma once

#include "Miner.h"
#include <iostream>
using namespace std;

//This is the BASE class for our states. We never use this as a state, as it should be purely an abstract class (made to be inheritted and it's methods overidden)
class State
{
public:
	State();
	~State();

	virtual void Execute(Miner *miner) = 0;
};

