#include "stdafx.h"
#include "Sing.h"

Sing::Sing()
{
}

Sing::~Sing()
{
}

void Sing::Execute(Canary* canary)
{
	cout << "Tweet, tweet, tweet" << endl;
	canary->c_Singing++;
}
