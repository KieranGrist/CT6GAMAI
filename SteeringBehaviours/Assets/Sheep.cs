using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringBehaviours))]
public class Sheep : MonoBehaviour {

    SteeringBehaviours SB;

	// Use this for initialization
	void Start ()
    {
        SB = GetComponent<SteeringBehaviours>();
        //  SB.WanderOn();
        SB.WallAvodienceOn();
        //SB.ObstacleAvodienceOn();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
