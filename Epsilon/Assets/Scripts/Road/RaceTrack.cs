using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class to store locations of checkpoints
/// </summary>
public class RaceTrack : MonoBehaviour
{
    public GameObject FinishLine, Sector1, Sector2; //Checkpoint game objects
    public static RaceTrack raceTrack; //Static reference to this script
    // Start is called before the first frame update

        /// <summary>
        /// Generates a new track
        /// </summary>
     void GenerateTrack()
    {

    }
    private void Awake()
    {
        raceTrack = this; //Set the reference to be this
    }
    // Update is called once per frame
    void Update()
    {
        raceTrack = this; //Set the reference to be this
    }
}
