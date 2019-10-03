// MinerApplication.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Miner.h"
#include "Canary.h"
#include <Windows.h>

int main()
{
	Miner *MainMiner = new Miner();
	Canary* MainBird = new Canary();
	MainMiner->Start();
	MainBird->Start();
	MainMiner->m_CanaryReference = MainBird;
	MainBird->c_MinerReference = MainMiner;
	while (1)
	{
		MainMiner->Update();
		MainBird->Update();
		Sleep(500); // wait 2000 milliseconds (2 seconds) before continuing
	}

	delete MainMiner;

    return 0;
}

