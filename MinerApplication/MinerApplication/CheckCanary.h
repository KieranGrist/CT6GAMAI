#pragma once
#include "State.h"
#include "Miner.h"
class CheckCanary : public  State<Miner>
{
public:
	CheckCanary();
	~CheckCanary();

	void Execute(Miner* miner);
};

