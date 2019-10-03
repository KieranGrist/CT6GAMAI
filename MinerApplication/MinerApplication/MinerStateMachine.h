#pragma once
template <class T>
class State;
class Canary;
class Miner;
class BankingGold;
class CheckCanary;
class GoAndEat;
class GoAndHaveADrink;
class GoHomeAndSleep;
class MiningForGold;
class MinerStateMachine
{
public:	
	MinerStateMachine();
	~MinerStateMachine();
	void Update();
	void ChangeState(State<Miner>* newState);
	void CheckState();

	void m_Canary();

	void Bank();

	void Eat();

	void Drink();

	void Home();

	void Mining();
	
	State<Miner>* PreviousState;
	Miner* miner;
	BankingGold* BankingState;
	CheckCanary* CanaryState;
	GoAndEat* EatState;
	GoAndHaveADrink* DrinkState;
	GoHomeAndSleep* HomeState;
	MiningForGold* MiningState;
};

