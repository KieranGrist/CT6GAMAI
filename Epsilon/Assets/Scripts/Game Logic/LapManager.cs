using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Used to keep track of the racing order
/// Helper class for state functionality
/// </summary>
public class LapManager : MonoBehaviour
{
    public Text Order;
    public static LapManager manager;
     List<Vehicle> CarPositions;
    List<Vehicle> RacingCars;


    public int Lap = 0;
    int VIC;
    public void StartRace()
    {
        RacingCars.AddRange(FindObjectsOfType<Vehicle>());
        foreach (var item in RacingCars)
            CarPositions.Add(item);
        foreach ( var item in RacingCars)
        {
            item.StartLap();
            item.CurrentLap = 0;
            Lap = 0;
            if (item.transform.position == RacingGrid.Grid.Grid1.transform.position)
            {
                CarPositions[0] = item;
                item.RacePosition = 0;
            }
            if (item.transform.position == RacingGrid.Grid.Grid2.transform.position)
            {
                CarPositions[1] = item;
                item.RacePosition = 1;
            }
            if (item.transform.position == RacingGrid.Grid.Grid3.transform.position)
            {
                CarPositions[2] = item;
                item.RacePosition = 2;
            }
            if (item.transform.position == RacingGrid.Grid.Grid4.transform.position)
            {
                CarPositions[3] = item;
                item.RacePosition = 3;
            }
            if (item.transform.position == RacingGrid.Grid.Grid5.transform.position)
            {
                CarPositions[4] = item;
                item.RacePosition = 4;
            }
            if (item.transform.position == RacingGrid.Grid.Grid6.transform.position)
            {
                CarPositions[5] = item;
                item.RacePosition = 5;
            }
            if (item.transform.position == RacingGrid.Grid.Grid7.transform.position)
            {
                CarPositions[6] = item;
                item.RacePosition = 6;
            }
            if (item.transform.position == RacingGrid.Grid.Grid8.transform.position)
            { 
                CarPositions[7] = item;
                item.RacePosition = 7;
            }

        }
    }
    private void Awake()
    {
        manager = this;
    }
    public void VehiclePassedCheckPoint(int WayPointNumber, Vehicle vehicle)
    {
        bool AlreadyChecked = false;
        if (vehicle.CurrentLap > Lap && WayPointNumber == 1) //Vehicle is on new lap
        {
            vehicle.EndLap();
            Lap = vehicle.CurrentLap;
            vehicle.RacePosition = 0;
       CarPositions[0] = vehicle;
            VIC = 1;
            vehicle.StartLap();
            AlreadyChecked = true;
        }
        if (WayPointNumber == 1 && !AlreadyChecked)
        {
                vehicle.EndLap();
                CarPositions[VIC] = vehicle;
            vehicle.RacePosition = VIC;
                VIC++;
                vehicle.StartLap();

        }
        if (Lap > 15 && WayPointNumber == 1)
        {
            vehicle.StopRacing();
        }
    }
    private void Update()
    {
        manager = this;
        Order.text = "Lap: " + Lap +"\n";
        foreach (var item in CarPositions)
            Order.text += item.RacePosition+1 + ": "+  item.name + " Lap Time " + item.CurrentLapTime + " " + item.Difference+ "\n";

    }

}
