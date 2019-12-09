using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggresive : AIAgent
{
    [Header("Aggresive Debug")]
    public float Fuel;
    public float FuelSubtacted, FuelInverse, FuelDivided;

    new void Update()
    {
        artieOverTake = 0;
        var Reference = steeringBehaviour.ProjectedCube.CheckForAI(vehicle);
        if (Reference)
        {
            var Desire = 0.5f;
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
        Fuel = vehicle.Fuel;
        FuelSubtacted = vehicle.Fuel - 100;
        FuelInverse = FuelSubtacted * -1;
        FuelDivided = FuelInverse / 100;
        float LapsLeft = LapManager.manager.MaxLaps - vehicle.CurrentLap;

        float PredictedFuelCost = vehicle.FuelUsedPerLap * LapsLeft;
        PredictedFuelCost *= 1.1f;
        if (PredictedFuelCost < vehicle.Fuel)
            artiePit = 0;
        else
            artiePit = FuelDivided;
        artieDrive = 1 - FuelDivided *.5f;
        if (vehicle.PerformingStop())
        {
            artieDrive = 0;
            artiePit = 1;
        }
        base.Update();

    }
}
