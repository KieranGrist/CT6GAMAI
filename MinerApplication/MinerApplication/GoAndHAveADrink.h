#pragma once
#include "State.h"
#include "Miner.h"
class GoAndHaveADrink : public  State<Miner>
{
public:
	GoAndHaveADrink();
	~GoAndHaveADrink();

	void Execute(Miner* miner);
};

