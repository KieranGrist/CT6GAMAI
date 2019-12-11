using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// List of directions the corner can be in
/// </summary>
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
/// <summary>
/// Class to controll corners in the road 
/// </summary>
public class Corner : Road
{
    //Direction the corner is in 
   public CornerDirection Direction;
    private void Update()
    {
        switch (Direction) //Switch between the corner 
        {
            case CornerDirection.NorthToEast:
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0); //Sets the rotation of the object
                break;
            case CornerDirection.NorthToWest:
                gameObject.transform.eulerAngles = new Vector3(0, 0 , 0);  //Sets the rotation of the object
                break;
            case CornerDirection.SouthToEast:
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);  //Sets the rotation of the object
                break;
            case CornerDirection.SouthToWest:
                gameObject.transform.eulerAngles = new Vector3(0, 270, 0);  //Sets the rotation of the object
                break;
            case CornerDirection.WestToNorth:
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);  //Sets the rotation of the object
                break;
            case CornerDirection.WestToSouth:
                gameObject.transform.eulerAngles = new Vector3(0, 270, 0);  //Sets the rotation of the object
                break; 
            case CornerDirection.EastToNorth:
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0);  //Sets the rotation of the object
                break;
            case CornerDirection.EastToSouth:
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);  //Sets the rotation of the object
                break;
        }
    }

}
