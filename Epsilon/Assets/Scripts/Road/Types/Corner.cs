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
[ExecuteInEditMode]
public class Corner : Road
{
    public CornerDirection Direction;
    private void Update()
    {
        switch (Direction)
        {
            case CornerDirection.NorthToEast:
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case CornerDirection.NorthToWest:
                gameObject.transform.eulerAngles = new Vector3(0, 0 , 0);
                break;
            case CornerDirection.SouthToEast:
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case CornerDirection.SouthToWest:
                gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
                break;
            case CornerDirection.WestToNorth:
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case CornerDirection.WestToSouth:
                gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
                break;
            case CornerDirection.EastToNorth:
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case CornerDirection.EastToSouth:
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
        }
    }

}
