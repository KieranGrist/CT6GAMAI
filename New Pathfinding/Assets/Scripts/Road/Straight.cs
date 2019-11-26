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
public class Straight : Road
{
    public RoadDirection roadDirection = RoadDirection.North;

    private void Update()
    {
        switch (roadDirection)
        { 
           case RoadDirection.North:
                gameObject
                break;
        }
    }
}