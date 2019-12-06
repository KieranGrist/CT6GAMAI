using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class NodeGenerator : MonoBehaviour
{
    [Header("Tile Map Settings")]
    public static NodeGenerator MapGen;
    public float GapBetweenNodes = 1;
    public int Area;
    public GameObject Cube;
    TimeSpan TimeCalculated;
    public float FunctionTime;
    List<GameObject> TempNodes = new List<GameObject>();
    private void Awake()
    {
        MapGen = this;
    }
    public void GenerateMap()
    {
        DateTime StartTime = DateTime.Now;
        float t = transform.position.x + (Area * GapBetweenNodes);
        t *= 0.5f;
        for (float x = transform.position.x - t; x < (transform.position.x + (Area * GapBetweenNodes)) * 0.5f; x += GapBetweenNodes)
            for (float z = transform.position.z - t; z < (transform.position.z + (Area * GapBetweenNodes)) * 0.5f; z += GapBetweenNodes)
            {
                GameObject go = Instantiate(Cube, new Vector3(x, 0, z), transform.rotation, transform);  
                go.transform.localScale = new Vector3(GapBetweenNodes, 0.01f, GapBetweenNodes);
                TimeCalculated = DateTime.Now - StartTime;
                FunctionTime = (float)TimeCalculated.TotalSeconds;
                if (FunctionTime >= 30)
                    break;
                TempNodes.Add(go);


            }
    }
    public void AddSelector()
    {
        foreach (var item in TempNodes)
            item.AddComponent<Selector>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.position.x + (Area * GapBetweenNodes), 0.01f, transform.position.z + (Area * GapBetweenNodes)));
    }

}
