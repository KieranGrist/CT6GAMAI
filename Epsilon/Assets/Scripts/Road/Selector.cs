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
    void Awake()
    {
        RoadSelector();
    }
    /// <summary>
    /// This should only run once the procedural generation has finished
    /// Only needs to be ran once!
    /// </summary>
    public void RoadSelector()
    {
        type = RoadTypes.Blank;
        bool Det = false;
        foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f), transform.rotation, LayerMask.GetMask("Walls")))
        {
            Det = true;
        }
        foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f), transform.rotation, LayerMask.GetMask("Obstacle")))
        {
            Det = true;
        }
            if (!Det)
            foreach (var item in Physics.OverlapBox(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f), transform.rotation, LayerMask.GetMask("Road")))
            {
                if (!GetComponent<RoadTile>())
                    gameObject.AddComponent<RoadTile>();
                if (item.GetComponent<Straight>())
                    type = RoadTypes.Straight;
                if (item.GetComponent<Corner>())
                    type = RoadTypes.Corner;
            }
        else
            Destroy(gameObject);
        if (type == RoadTypes.Blank)
            Destroy(gameObject);
    }
}
