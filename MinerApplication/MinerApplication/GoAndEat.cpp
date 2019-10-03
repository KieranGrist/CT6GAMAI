#include "stdafx.h"
#include "GoAndEat.h"
#include "MiningForGold.h"
GoAndEat::GoAndEat()
{
}

GoAndEat::~GoAndEat()
{
}

void GoAndEat::Execute(Miner* miner)
{
	cout << "Eating my Pasty" << endl;
	miner->m_Hunger--;

}
