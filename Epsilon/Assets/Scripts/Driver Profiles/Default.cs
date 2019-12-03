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

    
        Artie_OverTake = 0;
        Artie_Defend =0;
        Fuel = vehicle.Fuel;
        FuelSubtacted = vehicle.Fuel - 100;
        FuelInverse = FuelSubtacted * -1;
        FuelDivided = FuelInverse / 100;
        Artie_Pit = FuelDivided;
        Artie_GoForShortCut =-1; //If one exists
        Artie_GoForRandomItem =-1; //If exists

         Artie_Drive = 1 - FuelDivided;
        if (vehicle.PerformingStop())
        {
            Artie_Drive = 0;
            Artie_Pit = 1;
        }
        base.Update();

    }

}