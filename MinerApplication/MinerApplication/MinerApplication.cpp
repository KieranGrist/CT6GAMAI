// MinerApplication.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Miner.h"
#include <Windows.h>

int main()
{
	Miner *MainMiner = new Miner();
	MainMiner->Start();
	while (1)
	{
		MainMiner->Update();
		Sleep(2000); // wait 2000 milliseconds (2 seconds) before continuing
	}

	delete MainMiner;

    return 0;
}

