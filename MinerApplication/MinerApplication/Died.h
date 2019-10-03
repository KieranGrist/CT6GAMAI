#pragma once
#include "State.h"
#include "Canary.h"
class Died : public  State<Canary>
{
public:
	Died();
	~Died();

	void Execute(Canary* miner);
};

