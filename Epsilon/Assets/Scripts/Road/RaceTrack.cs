using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    public GameObject FinishLine, Sector1, Sector2;
    public static RaceTrack raceTrack;
    // Start is called before the first frame update

    public void GenerateTrack()
    {

    }
    private void Awake()
    {
        raceTrack = this;
    }
    // Update is called once per frame
    void Update()
    {
        raceTrack = this;
    }
}
