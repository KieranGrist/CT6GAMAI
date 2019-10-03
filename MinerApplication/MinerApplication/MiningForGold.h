#pragma once
#include "State.h"

//Mining for gold state
class MiningForGold :
	public State
{
public:
	MiningForGold();
	~MiningForGold();

	void Execute(Miner *miner);
};

