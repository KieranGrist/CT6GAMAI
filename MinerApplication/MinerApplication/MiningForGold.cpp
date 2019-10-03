#include "stdafx.h"
#include "MiningForGold.h"

MiningForGold::MiningForGold()
{
}


MiningForGold::~MiningForGold()
{
}

void MiningForGold::Execute(Miner *miner)
{
	//Print out information on what it is doing...
	cout << "Digging for gold!" << endl;

	//Increment the miner's gold amount
	miner->m_Gold++;


	// TO-DO: Check if a certain gold threshold has been reached, if so, then change to a different state using miner->ChangeState
	// This next state should make the Miner deposit gold into the bank (add m_Gold to m_BankedGold, then set m_Gold back to zero)
	// You will have to create a new class that inherits from "State" to achieve this (you can call this class "BankingGold")
	// Be sure to cout information at all stages so you can see how it is functioning
	
	//	How many other states can you add? Try adding variables like "Tiredness" and "Thirstiness" to the Miner, and make the
	//  the miner change to other states based on those values
	//	e.g. if (miner->m_Tiredness > 10) miner->ChangeState(new GoHomeAndSleep());
}
