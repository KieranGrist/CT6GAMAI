#include "stdafx.h"
#include "GoHomeAndSleep.h"
#include "MiningForGold.h"
GoHomeAndSleep::GoHomeAndSleep()
{
}

GoHomeAndSleep::~GoHomeAndSleep()
{
}

void GoHomeAndSleep::Execute(Miner* miner)
{
	cout << "I am at home and Sleeping" << endl;
	cout << "Sleep ZZZ" << endl;
	miner->m_Tiredness--;
}
