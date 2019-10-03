#pragma once
template <class T>
class State;
class Canary;
class Miner;
class Died;
class Fly;
class Sing;
class CanaryStateMachine
{
public:
	CanaryStateMachine();
	~CanaryStateMachine();
	void Update();
	void ChangeState(State<Canary>* newState);
	void CheckState();
	bool CheckDeath();

	void Death();

	void c_Fly();

	void c_Sing();

	State<Canary>* PreviousState;
	Canary* canary;
	Died* DeathState;
	Fly* FlyState;
	Sing* SingState;
};

