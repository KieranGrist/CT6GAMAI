#include "stdafx.h"
#include "Miner.h"
#include "MiningForGold.h"
#include "State.h"

Miner::Miner()
{
	//Set the intial state as MiningForGold
	pState = new MiningForGold();
}


Miner::~Miner()
{
	//When miner is deleted, we must delete the reference otherwise it will stay in memory
	delete pState;
}

void Miner::Update()
{
	pState->Execute(this);
}

void Miner::ChangeState(State *newState)
{
	delete pState;
	pState = newState;
}
