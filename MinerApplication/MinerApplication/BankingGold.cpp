#include "stdafx.h"
#include "BankingGold.h"
#include "MiningForGold.h"
BankingGold::BankingGold()
{
}

BankingGold::~BankingGold()
{
}

void BankingGold::Execute(Miner* miner)
{
	//Print out information of gold
	cout << "Banking Gold" << endl;
	//Banking the Miners gold
	miner->m_BankedGold+= miner->m_Gold;
	miner->m_Gold = 0;
}
