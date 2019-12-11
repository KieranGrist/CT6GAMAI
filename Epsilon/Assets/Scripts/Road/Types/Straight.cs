using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controlls which direction the road is facing in 
/// </summary>
public enum RoadDirection
{
    North,
    East,
    South,
    West
}
[ExecuteInEditMode]

/// <summary>
/// Class to control straights
/// </summary>
public class Straight : Road
{
 public    RoadDirection roadDirection = RoadDirection.North; //Road direction
    private void Update()
    {
        switch (roadDirection) //Switch the road direction
        { 
           case RoadDirection.North:
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case RoadDirection.East:
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case RoadDirection.South:
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case RoadDirection.West:
                gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
                break;
        }
    }
}