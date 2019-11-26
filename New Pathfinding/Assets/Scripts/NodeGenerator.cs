using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class NodeGenerator : MonoBehaviour
{
    [Header("Tile Map Settings")]
    public GameObject Cube;
    public List<GameObject> GO = new List<GameObject>();
    public float GapBetweenNodes = 1;
    public float PreviousGapBetweenNodes = 0;
    public int Area;
    public int PreviousArea = 0;
    public TimeSpan TimeCalculated;
    public bool Generated = false;
    [Header("Tile Loop Time")]
    public List<float> FunctionTimes = new List<float>();
    public float AverageTileTime;
    public float TotalTileTime;
    public float TileMaxTime;
    [Header("Reset Loop Time")]
    public List<float> ResetFunctionTimes = new List<float>();
    public float AverageResetTime;
    public float TotalResetTime;

    float Timer; 
    void GenerateCube()
    {
        FunctionTimes.Clear();
        ResetFunctionTimes.Clear();
        foreach (var item in GO)
            DestroyImmediate(item);
        GO.Clear();
        int i = 0;
        for (float x = transform.position.x; x < transform.position.x + (Area * GapBetweenNodes); x += GapBetweenNodes)
            for (float z = transform.position.z; z < transform.position.z + (Area * GapBetweenNodes); z += GapBetweenNodes)
            {
                DateTime StartTime = DateTime.Now;
                GameObject go = Instantiate(Cube, new Vector3(x, 0, z), transform.rotation,transform);
                go.AddComponent<Selector>();
                i++;
                go.transform.localScale = new Vector3(GapBetweenNodes, 0.01f, GapBetweenNodes);
                GO.Add(go);
                TimeCalculated = DateTime.Now - StartTime;
                FunctionTimes.Add((float)TimeCalculated.TotalMilliseconds);      
            }

        float[] arr  = FunctionTimes.ToArray();
        float sum = 0;
        TotalTileTime = 0;
        TileMaxTime = float.MinValue;
        for (i = 0; i < arr.Length; i++)
        {
            if (arr[i] > TileMaxTime)
                TileMaxTime = arr[i];
            TotalTileTime += arr[i] ;
            sum += arr[i];
        }
        AverageTileTime = sum / arr.Length;
        Generated = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying) 
          if ((Area != PreviousArea || GapBetweenNodes != PreviousGapBetweenNodes))
                 GenerateCube();          
        PreviousArea = Area;
        PreviousGapBetweenNodes = GapBetweenNodes;
    }
}
