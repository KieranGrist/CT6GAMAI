using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pipeline : MonoBehaviour { 

   
    void Start()
    {    
        NodeGenerator.MapGen.GenerateMap();
        //RaceTrack.raceTrack.GenerateTrack();
        //ObstabcleGenerator.ObstabclesGen.GenerateObstabcles();
        NodeGenerator.MapGen.AddSelector();    
       ArtieGenerator.AIGen.PlaceAIUnits();
        NavGraph.map.GenerateNavMesh();
        ArtieGenerator.AIGen.AIPersonalitySelector();
        LapManager.manager.StartRace();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
