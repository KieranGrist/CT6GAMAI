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
  public  List<Vehicle> CarPositions = new List<Vehicle>();
    public float MaxLaps=15;

    public int Lap = 0;
    int VIC;
    public void StartRace()
    {
        CarPositions = new List<Vehicle>();
        foreach (var item in FindObjectsOfType<Vehicle>())
        {
            
            CarPositions.Add(item);
            item.StartLap();
            item.RacePosition = 0;
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
            CarPositions.Clear();
            CarPositions.Add(vehicle);
            VIC = 1;
            vehicle.StartLap();
            AlreadyChecked = true;
        }
        if (WayPointNumber == 1 && !AlreadyChecked && vehicle.CurrentLap == Lap)
        {
                vehicle.EndLap();
            CarPositions.Add(vehicle);
            vehicle.RacePosition = VIC;
                VIC++;
                vehicle.StartLap();

        }
        if (Lap > MaxLaps && WayPointNumber == 1)
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
