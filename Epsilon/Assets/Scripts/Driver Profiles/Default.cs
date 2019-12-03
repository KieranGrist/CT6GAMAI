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
        if (vehicle.RacePosition != 0)
        {
            var R = LapManager.manager.CarPositions[vehicle.RacePosition - 1].transform;

            if (Vector3.Distance(transform.position, R.transform.position) < 1)
            {
                var Desire = 0.0f;
                var Reference = LapManager.manager.CarPositions[vehicle.RacePosition - 1];
                if (Reference.MaxSpeed < vehicle.MaxSpeed)
                    Desire += 0.5f;
                if (Reference.Mass > vehicle.Mass)
                    Desire += 0.5f;
                if (Reference.Fuel < vehicle.Fuel)
                    Desire += 0.5f;
                if (Reference.CurrentLapTime > vehicle.CurrentLap)
                    Desire += 0.5f;
                artieOverTake = Desire;
            }
        }   
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