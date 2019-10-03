#include "stdafx.h"
#include "Miner.h"
#include "CheckCanary.h"
#include "GoAndEat.h"
#include "GoAndHAveADrink.h"
#include "GoHomeAndSleep.h"
#include "MiningForGold.h"
#include "BankingGold.h"
#include "State.h"
#include "Canary.h"

Miner::Miner()
{
}


Miner::~Miner()
{
	//When miner is deleted, we must delete the reference otherwise it will stay in memory
	delete pState;
}

void Miner::Start()
{
	m_StateMachine.BankingState = new BankingGold();
	m_StateMachine.CanaryState = new CheckCanary();
	m_StateMachine.EatState = new GoAndEat();
	m_StateMachine.DrinkState = new GoAndHaveADrink();
	m_StateMachine.HomeState = new GoHomeAndSleep();
	m_StateMachine.MiningState = new MiningForGold();
	pState = m_StateMachine.MiningState;
}

void Miner::Update()
{
	m_StateMachine.miner = this;
	m_StateMachine.Update();
	pState->Execute(this);
}

