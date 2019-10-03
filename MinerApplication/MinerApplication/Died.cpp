#include "stdafx.h"
#include "Died.h"
#include "Canary.h"
#include "Miner.h"
Died::Died()
{
}

Died::~Died()
{
}

void Died::Execute(Canary* canary)
{
	canary->c_Dead = canary->c_MinerReference->m_Day;

}
