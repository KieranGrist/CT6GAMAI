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

        Artie_Drive = Helper.DistanceToItem(transform, TargetNode.transform); //The AI will always have some basic want to get to their target node as this is what makes them fast
        Artie_OverTake = 0;
        Artie_Defend =0;
        Fuel = vehicle.Fuel;
        FuelSubtacted = vehicle.Fuel - 100;
        FuelInverse = FuelSubtacted * -1;
        FuelDivided = FuelInverse / 10;
        Artie_Pit = 0; // ((vehicle.Fuel - 100) * -1) / 10; 
        Artie_GoForShortCut =-1; //If one exists
        Artie_GoForRandomItem =-1; //If exists
        base.Update();

    }

}