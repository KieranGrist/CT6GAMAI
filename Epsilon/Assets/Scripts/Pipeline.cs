using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pipeline : MonoBehaviour {

    public bool GenerateTrack = false;
    public bool GenerateObstacles = false;

    void Start()
    {    
        //NodeGenerator.MapGen.GenerateMap();
        if (GenerateTrack)
        RaceTrack.raceTrack.GenerateTrack();
        if (GenerateObstacles)
            RaceTrack.raceTrack.GenerateObstacles();

       // NodeGenerator.MapGen.AddSelector();    
       //ArtieGenerator.AIGen.PlaceAIUnits();
       // NavGraph.map.GenerateNavMesh();
       // ArtieGenerator.AIGen.AIPersonalitySelector();
       // LapManager.manager.StartRace();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
