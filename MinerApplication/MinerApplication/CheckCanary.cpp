#include "stdafx.h"
#include "CheckCanary.h"
#include "Canary.h"
#include "Miner.h"
CheckCanary::CheckCanary()
{
}

CheckCanary::~CheckCanary()
{
}

void CheckCanary::Execute(Miner* miner)
{
	
	miner->m_CheckedCanary = true;
	miner->m_CanaryReference->c_StateMachine.CheckDeath();
	cout << "I am checking the canary" << endl;
	if (miner->m_CanaryReference->IsDead == true)
	{
		if (miner->FirstTime)
		{
			miner->m_Day = 0;
			miner->FirstTime = false;
		}
		cout << "*ALARM* *ALARM* THE BIRD HAS DIED, MINE WILL CLOSE FOR 5 DAYS" << " This is Day" << miner->m_CanaryReference->c_Dead << endl;
	}
	else
	{
		cout << "The canary is alive and well" << endl;
	}

	//The state for the bird is changed somewhere else and this can lead to loop back errors.
}
