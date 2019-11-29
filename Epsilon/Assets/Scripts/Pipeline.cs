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

        StartTime = DateTime.Now;
        NavGraph.map.GenerateNavMesh();
        TimeCalculated = DateTime.Now - StartTime;
        NavMeshTime = (float)TimeCalculated.Milliseconds;

var temp=        ArtieGenerator.AIGen.PlaceAIUnits();

        ArtieGenerator.AIGen.AIPersonalitySelector();
        var T = 0; var I = 0;
        foreach (var item in Grids.Grid.GridList)
        {
            if (!item.Key)
            {
                T = I;
                break;
            }       
            I++;
        }
        switch (temp)
        {
            case "Ferrari":
                Player.player.gameObject.AddComponent<Ferrari>();
                break;
            case "Mercedes":
                Player.player.gameObject.AddComponent<Mercedes>();
  
                break;
            case "Ford":
                Player.player.gameObject.AddComponent<Ford>();
   
                break;
            case "Renault":
                Player.player.gameObject.AddComponent<Renault>();
        
                break;
        }
        Player.player.PlacePlayer(T);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
