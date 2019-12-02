using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Pipeline : MonoBehaviour
{    //Pipeline : Create Node Tile Map ->  Generate Racetrack -> Generate Obstabcles -> Selector -> Generate Nav Mash - > Place AI units -> AI units select personality -> Place Player -> AI Unit Pathfinding -> Play!
    TimeSpan TimeCalculated;
    public float MapGenTime;
    public float SelectorTime;
    public float NavMeshTime;
    // Start is called before the first frame update
   
    void Start()
    {
        var StartTime = DateTime.Now;       
        NodeGenerator.MapGen.GenerateMap();
        TimeCalculated = DateTime.Now - StartTime;
        MapGenTime = (float)TimeCalculated.Milliseconds;
        //RaceTrack.raceTrack.GenerateTrack();
        //ObstabcleGenerator.ObstabclesGen.GenerateObstabcles();
        StartTime = DateTime.Now;
        NodeGenerator.MapGen.AddSelector();
        TimeCalculated = DateTime.Now - StartTime;
        SelectorTime = (float)TimeCalculated.Milliseconds;

     

       ArtieGenerator.AIGen.PlaceAIUnits();
        StartTime = DateTime.Now;
        NavGraph.map.GenerateNavMesh();
        TimeCalculated = DateTime.Now - StartTime;
        NavMeshTime = (float)TimeCalculated.Milliseconds;
        ArtieGenerator.AIGen.AIPersonalitySelector();
        var T = 0; var I = 0;
        foreach (var item in RacingGrid.Grid.GridList)
        {
            if (!item.Key)
            {
                T = I;
                break;
            }       
            I++;
        }


        LapManager.manager.StartRace();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
