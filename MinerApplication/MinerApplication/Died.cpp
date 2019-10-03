#include "stdafx.h"
#include "Died.h"

Died::Died()
{
}

Died::~Died()
{
}

void Died::Execute(Canary* miner)
{
	miner->c_Dead++;
}
