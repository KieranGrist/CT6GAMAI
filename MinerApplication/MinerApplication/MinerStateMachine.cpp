#include "stdafx.h"
#include "MinerStateMachine.h"
#include "State.h"
#include "BankingGold.h"
#include "CheckCanary.h"
#include "GoAndEat.h"
#include "GoAndHAveADrink.h"
#include "GoHomeAndSleep.h"
#include "MiningForGold.h"
#include "Miner.h"
#include "CanaryStateMachine.h"
#include "Canary.h"
MinerStateMachine::MinerStateMachine()
{
	//Initalise Previous state to any state so it can be deleted for change state
	PreviousState = new CheckCanary();
}

MinerStateMachine::~MinerStateMachine()
{
}

void MinerStateMachine::Update()
{
	
	CheckState();
}


void MinerStateMachine::ChangeState(State<Miner>* newState)
{
	PreviousState = NULL;
	PreviousState = miner->pState;

	miner->pState = NULL;
	miner->pState = newState;
}

void MinerStateMachine::CheckState()
{
		if (miner->pState == BankingState)
		{
			Bank();
		}

		if (miner->pState == EatState)
		{
			Eat();
		}
		if (miner->pState == DrinkState)
		{
			Drink();
		}
		if (miner->pState == HomeState)
		{
			Home();
		}
		if (miner->pState == MiningState)
		{
			Mining();
		}
		if (miner->pState == CanaryState)
		{
			m_Canary();
		}
}

void MinerStateMachine::m_Canary()
{
	if (miner->m_CheckedCanary)
	{
		ChangeState(PreviousState);
		miner->m_CheckedCanary = false;
	}
}

void MinerStateMachine::Bank()
{
	if (miner->m_Gold == 0)
	{	
		cout << "I have banked all the gold, I now have " << miner->m_BankedGold << " Stored in the bank account" << " I will carry on mining" << endl;
		ChangeState(MiningState);
	}
}

void MinerStateMachine::Eat()
{
	if (miner->m_Hunger <= 0)
	{
		cout << "I have eaten and will now return" << endl;
		ChangeState(PreviousState);
		miner->m_Hunger = 0;
	}
}

void MinerStateMachine::Drink()
{
	if (miner->m_Thirstiness <= 0)
	{
		cout << "I have drinken and will now return" << endl;
		ChangeState(PreviousState);
		miner->m_Thirstiness = 0;
	}
}

void MinerStateMachine::Home()
{
	bool ChangedState = false;
	if (BirdChecked == false)
	{
		cout << "The day has now ended, as such, you are sent to check the canary" << endl;
		ChangeState(CanaryState);
		BirdChecked = true;
	}
	else
	{
		cout << "The Bird Has been checked " << endl;
		miner->m_Day++;
		if (miner->m_Hunger > 0)
		{
			cout << "I need food, i will now go and eat" << endl;
			ChangeState(EatState);
			ChangedState = true;
		}
		if (!ChangedState)
		{
			if (miner->m_Thirstiness > 0)
			{
				cout << "I need drink, i will now go and drink" << endl;
				ChangeState(DrinkState);
			}
		}
		if (!ChangedState)
		{
			if (miner->m_Tiredness <= 0)
			{

				cout << "I am at now ready for a new days work, I will now head ot the mine" << endl;
				ChangeState(MiningState);
				miner->m_Tiredness = 0;
				BirdChecked = false;
			}
		}
	}
}

void MinerStateMachine::Mining()
{
	bool ChangedStates = false;

	if (!ChangedStates)
	{
		if (miner->m_Gold >= 10)
		{
			ChangeState(BankingState);
			cout << "I have no more room for my gold, I must go to the bank" << endl;
			ChangedStates = true;
		}
	}
	if (!ChangedStates)
	{
		if (miner->m_Tiredness >= 20)
		{
			ChangeState(HomeState);
			cout << "I am tired I now need to go home and sleep" << endl;
			ChangedStates = true;
		}
	}
	if (!ChangedStates)
	{
		if (miner->m_Thirstiness >= 15)
		{
			ChangeState(DrinkState);
			cout << "I am thirsty I must go and have a drink" << endl;
			ChangedStates = true;
		}
	}
	if (!ChangedStates)
	{
		if (miner->m_Hunger >= 13)
		{
			ChangeState(EatState);
			ChangedStates = true;
		}
	}
}


