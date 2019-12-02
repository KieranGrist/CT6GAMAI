using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Straight has a ENUM direction which controls what way the AI pathfinding can go 
 */
public enum RoadDirection
{
    North,
    East,
    South,
    West
}
[ExecuteInEditMode]
public class Straight : Road
{
    public RoadDirection roadDirection = RoadDirection.North;
    private void Update()
    {
        switch (roadDirection)
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
        gameObject.layer = 9;
    }
}