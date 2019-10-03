#pragma once
#include "State.h"
#include "Miner.h"

//Banking gold state
class BankingGold : public State<Miner>
{

public:
	BankingGold();
	~BankingGold();


	void Execute(Miner* miner);
};

