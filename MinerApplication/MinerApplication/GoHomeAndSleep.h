#pragma once
#include "State.h"
#include "Miner.h"
class GoHomeAndSleep : public  State<Miner>
{
public:
	GoHomeAndSleep();
	~GoHomeAndSleep();

	void Execute(Miner* miner);
};

