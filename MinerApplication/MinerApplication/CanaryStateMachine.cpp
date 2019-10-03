#include "stdafx.h"
#include "CanaryStateMachine.h"
#include "State.h"
#include "Fly.h"
#include "Died.h"
#include "Sing.h"
#include <time.h>
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

void CanaryStateMachine::Death()
{
	if (canary->c_Dead > 4)
	{
		ChangeState(PreviousState);
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
	if (random > 60)
	{
		cout << "The bird has died from poision gas" << endl;
		ChangeState(DeathState);
		StateChanged = true;
	}
	if (!StateChanged)
	{
		if (canary->c_Flying > 10)
		{
			cout << "The bird is tired of flying and is now going to sing" << endl;
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
	if (random > 60)
	{
		cout << "The bird has died from poision gas" << endl;
		ChangeState(DeathState);
		StateChanged = true;
	}
	if (!StateChanged)
	{
		if (canary->c_Singing > 10)
		{
			cout << "The bird is tired of singing and wants to stretch its wings " << endl;
		}
	}
}
