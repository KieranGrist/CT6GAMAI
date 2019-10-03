#pragma once
#include "State.h"
#include "Canary.h"
class Fly : public  State<Canary>
{
public:
	Fly();
	~Fly();

	void Execute(Canary* miner);
};