using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default : AIAgent
{
    [Header("Default Debug")]
    public float Fuel;
    public float FuelSubtacted, FuelInverse, FuelDivided;
        
    new void Update()
    {

    
        artieOverTake = 0;
        artieDefend =0;
        Fuel = vehicle.Fuel;
        FuelSubtacted = vehicle.Fuel - 100;
        FuelInverse = FuelSubtacted * -1;
        FuelDivided = FuelInverse / 100;
        artiePit = FuelDivided;
        artieGoForShortCut =-1; //If one exists
        artieGoForRandomItem =-1; //If exists

         artieDrive = 1 - FuelDivided;
        if (vehicle.PerformingStop())
        {
            artieDrive = 0;
            artiePit = 1;
        }
        base.Update();

    }

}