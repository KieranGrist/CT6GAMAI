#include "stdafx.h"
#include "GoAndHaveADrink.h"
#include "MiningForGold.h"
GoAndHaveADrink::GoAndHaveADrink()
{
}

GoAndHaveADrink::~GoAndHaveADrink()
{
}

void GoAndHaveADrink::Execute(Miner* miner)
{
	cout << "Having A Drink " << endl;
	miner->m_Thirstiness--;
}
