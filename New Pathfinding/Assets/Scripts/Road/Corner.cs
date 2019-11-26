using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CornerDirection
{

    NorthToEast,
    NorthToWest,
    SouthToEast,
    SouthToWest,
    WestToNorth,
    WestToSouth,
        EastToNorth,
        EastToSouth
}
public class Corner : Road
{
    CornerDirection Direction;
        public override void Reset()
        {



        }

}
