using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Used to keep track of the racing order
/// Helper class for state functionality
/// </summary>
public class LapManager : MonoBehaviour
{
    public static LapManager manager;
    public List<Vehicle> CarPositions;
    public Text Order;
    public List<Vehicle> RacingCars;
    public int Lap = 0;
    public int CurrentWayPont =4 ;
    int PreviousWayPont = 3;
    public void StartRace()
    {
        RacingCars.AddRange(FindObjectsOfType<Vehicle>());
        foreach (var item in RacingCars)
            CarPositions.Add(item);
        foreach ( var item in RacingCars)
        {
      
            if (item.transform.position == RacingGrid.Grid.Grid1.transform.position)
            {
                CarPositions[0] = item;
                item.Position = 0;
            }
            if (item.transform.position == RacingGrid.Grid.Grid2.transform.position)
            {
                CarPositions[1] = item;
                item.Position = 1;
            }
            if (item.transform.position == RacingGrid.Grid.Grid3.transform.position)
            {
                CarPositions[2] = item;
                item.Position = 2;
            }
            if (item.transform.position == RacingGrid.Grid.Grid4.transform.position)
            {
                CarPositions[3] = item;
                item.Position = 3;
            }
            if (item.transform.position == RacingGrid.Grid.Grid5.transform.position)
            {
                CarPositions[4] = item;
                item.Position = 4;
            }
            if (item.transform.position == RacingGrid.Grid.Grid6.transform.position)
            {
                CarPositions[5] = item;
                item.Position = 5;
            }
            if (item.transform.position == RacingGrid.Grid.Grid7.transform.position)
            {
                CarPositions[6] = item;
                item.Position = 6;
            }
            if (item.transform.position == RacingGrid.Grid.Grid8.transform.position)
            { 
                CarPositions[7] = item;
                item.Position = 7;
            }

        }
    }
    private void Awake()
    {
        PreviousWayPont = 3;
        manager = this;
    }
    public int VehiclePassedCheckPoint(int WayPointNumber, Vehicle vehicle)
    {
         //Create a run an algoritum which correctly tracks vehicles around the track
        return 0;

    }
    private void Update()
    {
        manager = this;
        //Order.text = "";
        //foreach (var item in CarPositions)
        //    Order.text += item.name + "\n";

    }

}
