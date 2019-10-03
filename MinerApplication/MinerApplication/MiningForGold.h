#pragma once
#include "State.h"
#include "Miner.h"
//Mining for gold state
class MiningForGold :
	public State<Miner>
{
public:
	MiningForGold();
	~MiningForGold();

	void Execute(Miner *miner);
};

