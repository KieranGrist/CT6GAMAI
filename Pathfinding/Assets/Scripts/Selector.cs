using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RoadTypes
{
    Blank,
    Straight,
    Corner,
    RoadtypeMax
}
[ExecuteInEditMode]
public class Selector : MonoBehaviour
{
    public RoadTypes type = RoadTypes.Blank;
    public RoadTypes PreviousType;
    // Start is called before the first frame update

    private void Update()
    {
        
    }
}
