#pragma once
#include "State.h"
#include "Miner.h"
class GoAndEat : public  State<Miner>
{
public:
	GoAndEat();
	~GoAndEat();

	void Execute(Miner* miner);
};

