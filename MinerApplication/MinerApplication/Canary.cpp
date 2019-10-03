#include "stdafx.h"
#include "Canary.h"
#include "Miner.h"
#include "Died.h"
#include "Fly.h"
#include "Sing.h"
Canary::Canary()
{
}

Canary::~Canary()
{
}
void Canary::Start()
{
	
	 c_Singing =0;
	 c_Flying =0;
	 c_Dead =0;
	 c_StateMachine.DeathState = new Died();
	 c_StateMachine.FlyState = new Fly();
	 c_StateMachine.SingState = new Sing();
	 c_StateMachine.PreviousState = new Sing();
	 pState = c_StateMachine.FlyState;
}
void Canary::Update()
{
	cout << endl << endl;
	c_StateMachine.canary = this;
	c_StateMachine.Update();
	pState->Execute(this);
}

void Canary::ChangeState(State<Canary>* newState)
{
}
