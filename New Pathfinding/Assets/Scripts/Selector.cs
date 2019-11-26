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
  public  RoadTypes PreviousType;

    // Start is called before the first frame update
    void Start()
    {

    }
    void TileCreator()
    {
        if (type != PreviousType)
        {
            if (gameObject.GetComponent<Node>())
                DestroyImmediate(gameObject.GetComponent<Node>());     
            switch (type)
            {
                case RoadTypes.RoadtypeMax:
                case RoadTypes.Blank:
                    if (gameObject.GetComponent<Rigidbody>())
                        DestroyImmediate(gameObject.GetComponent<Rigidbody>());
           
                    break;
                case RoadTypes.Straight:
                    gameObject.AddComponent<Straight>();
                    break;
                case RoadTypes.Corner:
                    gameObject.AddComponent<Corner>();
                    break;
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying && type == RoadTypes.Blank)
        GetComponent<Renderer>().material.color = Color.green;
        TileCreator();
        PreviousType = type;
    }
}
