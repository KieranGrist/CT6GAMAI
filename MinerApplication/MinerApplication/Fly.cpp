#include "stdafx.h"
#include "Fly.h"

Fly::Fly()
{
}

Fly::~Fly()
{
}

void Fly::Execute(Canary* canary)
{
	cout << "The Bird flys around the cage, energetically" << endl;	
	canary->c_Flying++;
}
