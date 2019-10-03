#pragma once
#include "State.h"
#include "Canary.h"
class Sing : public  State<Canary>
{
public:
	Sing();
	~Sing();

	void Execute(Canary* miner);
};