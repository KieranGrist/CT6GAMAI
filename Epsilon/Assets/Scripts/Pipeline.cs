using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that handles what order the world procedures are done in
/// </summary>
public class Pipeline : MonoBehaviour {    
    public bool GenerateTrack = false;
    public bool GenerateObstacles = false;
    void Start()
    {    
        if (GenerateTrack)
        RaceTrack.raceTrack.GenerateTrack();
        if (GenerateObstacles)
            RaceTrack.raceTrack.GenerateObstacles();

      //  NodeGenerator.MapGen.GenerateMap(); //Generate a map 
       // NodeGenerator.MapGen.AddSelector();   //Add selectors to nodes
       //ArtieGenerator.AIGen.PlaceAIUnits(); //Place the AI units 
       // NavGraph.map.GenerateNavMesh(); //Generate the nav mesh 
       // ArtieGenerator.AIGen.AIPersonalitySelector(); // Add driver profiles 
        //LapManager.manager.StartRace(); //Start the race 
    }
}
