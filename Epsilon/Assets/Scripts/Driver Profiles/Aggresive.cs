using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Aggresive driver profile
/// </summary>
public class Aggresive : AIAgent
{


    new void Update()
    {
        artieOverTake = 0; //Set overtake desire to 0 
        var Reference = steeringBehaviour.ProjectedCube.CheckForAI(vehicle); //Check for AIS in projected cube
        if (Reference) //If reference exists 
        {
            var Desire = 0.5f; //Set desire to be 0.5f 
            if (Reference.MaxSpeed < vehicle.MaxSpeed) //if it is faster then the reference
                Desire += 0.5f; //Increase desire by .5
            if (Reference.Mass > vehicle.Mass)  //If it is lighter then the reference
                Desire += 0.5f; //Increase desire by .5
            if (Reference.Fuel < vehicle.Fuel)  //If it has less fuel then the reference
                Desire += 0.5f; //Increase desire by .5
            if (Reference.CurrentLapTime > vehicle.CurrentLap)  //If it is laping quicker
                Desire += 0.5f;
            artieOverTake = Desire;

        }
        var  Fuel = vehicle.Fuel; //Store the current fuel of the vehicle 
        var FuelSubtacted = vehicle.Fuel - 100; //Subtract the fuel by 100 
        var FuelInverse = FuelSubtacted * -1; //increase the subtracted value 
        var FuelDivided = FuelInverse / 100; //divde the inverse by 100
        var LapsLeft = LapManager.manager.MaxLaps - vehicle.CurrentLap; //Get the max laps and subtract it by the current lap, this will get how many laps are left

        var PredictedFuelCost = vehicle.FuelUsedPerLap * LapsLeft; //Get the vehicles fuel usage by lap and then multiply it by the laps left, this is a predicition of how much fuel is needed to finish the race
        PredictedFuelCost /= 1.1f; //Divide the predicted fuel by 1.1 so the AI will try and stretch out the fuel by another lap
        if (PredictedFuelCost < vehicle.Fuel) //If predicted fuel is less then current vehicle fuel carry on
            artiePit = 0; //Set pit desire to be 0
        else
            artiePit = FuelDivided; //Set pit desire to be the fuel divded
        artieDrive = 1 - FuelDivided *.5f; //Set the drive desire to be one subtracted by fuel divded halved
        if (vehicle.PerformingStop()) //If performing a stop
        {
            artieDrive = 0; //Dont drive
            artiePit = 1; //Carry on with the stop
        }
        base.Update(); //Call on the base update

    }
}
