#include "stdafx.h"
#include "CanaryStateMachine.h"
#include "State.h"
#include "Fly.h"
#include "Died.h"
#include "Sing.h"
#include <time.h>
#include "Miner.h"
#include "Canary.h"
#include <random>
CanaryStateMachine::CanaryStateMachine()
{
}

CanaryStateMachine::~CanaryStateMachine()
{
}

void CanaryStateMachine::Update()
{
	CheckState();
}

void CanaryStateMachine::ChangeState(State<Canary>* newState)
{
	PreviousState = NULL;
	PreviousState = canary->pState;

	canary->pState = NULL;
	canary->pState = newState;
}

void CanaryStateMachine::CheckState()
{
	CheckDeath();
	if (canary->pState == FlyState)
	{
		c_Fly();
	}
	if (canary->pState == DeathState)
	{
		Death();
	}
	if (canary->pState == SingState)
	{
		c_Sing();
	}
}

bool CanaryStateMachine::CheckDeath()
{

	if (canary->pState == DeathState||canary->IsDead)
	{
		return true;
		canary->IsDead = true;
		ChangeState(DeathState);
	}
	else
	{
		canary->c_MinerReference->FirstTime = true;
		canary->IsDead = false;
		return false;
	}
}

void CanaryStateMachine::Death()
{
	if (canary->c_Dead >= 5)
	{
		ChangeState(PreviousState);
		canary->c_Dead = 0;
	}
}

void CanaryStateMachine::c_Fly()
{
	srand(time(NULL));
	int random = rand() % 100;
	random = rand() % 100;
	random = rand() % 100;
	random = rand() % 100;
	bool StateChanged = false;
	if (random >= 90)
	{
		canary->IsDead = true;
		cout << "The bird has died from poision gas" << endl;
		ChangeState(DeathState);
		StateChanged = true;
	}
	if (!StateChanged)
	{
		if (canary->c_Flying > 10)
		{
			canary->IsDead = true;
			cout << "The bird is tired of flying and is now going to sing" << endl;
			canary->c_Flying = 0;
			ChangeState(SingState);
		}
	}
}

void CanaryStateMachine::c_Sing()
{
	srand(time(NULL));
	int random = rand() % 100;
	random = rand() % 100;
	random = rand() % 100;
	random = rand() % 100;
	bool StateChanged = false;
	if (random >= 90)
	{
		canary->IsDead = true;
		cout << "The bird has died from poision gas" << endl;
		ChangeState(DeathState);
		StateChanged = true;
	}
	if (!StateChanged)
	{
		if (canary->c_Singing > 10)
		{
			cout << "The bird is tired of singing and wants to stretch its wings " << endl;
			canary->c_Singing = 0;
			ChangeState(FlyState);
		}
	}
}
