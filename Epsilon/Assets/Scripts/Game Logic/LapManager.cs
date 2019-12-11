using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Used to keep track of the racing order
/// Helper class for state functionality
/// </summary>
public class LapManager : MonoBehaviour
{
    public Text Order; //Reference of order tex
    public static LapManager manager;  //Static reference of lap manager
    List<Vehicle> CarPositions = new List<Vehicle>(); //List of car posoitions
    public float MaxLaps=15; //How many laps to race 

     int Lap = 0; //Current lap
    int Position; //Position of the next car
    /// <summary>
    /// Start the race for the vehicles
    /// </summary>
    public void StartRace()
    {
        CarPositions = new List<Vehicle>(); //Create a new list of cars
        foreach (var item in FindObjectsOfType<Vehicle>()) //loop through this vehicles
        {
            CarPositions.Add(item);//add the item to the car positions
            item.StartLap(); //Start the lap timer 
            item.RacePosition = 0; //Set the race position to be 0
        }
    }
    private void Awake()
    {
        manager = this; //Set reference to be this 
    }

    /// <summary>
    /// Called when a vehicle has passed a checkpoint
    /// Used to track car positions
    /// </summary>
    /// <param name="WayPointNumber"></param>
    /// <param name="vehicle"></param>
    public void VehiclePassedCheckPoint(int WayPointNumber, Vehicle vehicle)
    {
        bool AlreadyChecked = false; //create a bool to check if the vehicle has been checked yet
        if (vehicle.CurrentLap > Lap && WayPointNumber == 1) //Vehicle is on new lap and has passed the finish line
        {
            vehicle.EndLap(); //End the tumer
            Lap = vehicle.CurrentLap;  //Set the lap to be the lap this vehicle is on 
            vehicle.RacePosition = 0; //Set thhe race position to be 0
            CarPositions.Clear(); //Clear the list of cars
            CarPositions.Add(vehicle); //Add the vehicle to the car position list 
            Position = 1; //set the Position to be 1 
            vehicle.StartLap(); //Start a new lap 
            AlreadyChecked = true; //Set already check to be true
        }
        if (WayPointNumber == 1 && !AlreadyChecked && vehicle.CurrentLap == Lap) //If checkpoint is a finish line, if it hasnt veen checked and the vehicle is on the current lap 
        {
                vehicle.EndLap(); //end the vehicle lap 
            CarPositions.Add(vehicle); // add the vehicle to the position list
            vehicle.RacePosition = Position; //set the race position to be position
                Position++; //Increase position 
            vehicle.StartLap(); //Start a new lap

        }
        if (Lap > MaxLaps && WayPointNumber == 1) //if the vehicle has succedded the max number of laps 
        { 
            vehicle.StopRacing(); // Stop the AI from racing
        }
    }
    private void Update()
    {
        manager = this; //Set reference to be this 
        Order.text = "Lap: " + Lap +"\n"; //Set text to be Lap: X
        foreach (var item in CarPositions) //Loop through the car positions list
            Order.text += item.RacePosition+1 + ": "+  item.name + " Lap Time " + item.CurrentLapTime + " " + item.Difference+ "\n"; //Add a new line of text to the order saying "X: Name + Time: T  D"
    }

}
