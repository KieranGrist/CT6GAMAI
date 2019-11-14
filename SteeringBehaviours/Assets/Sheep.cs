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

        //Move to Pos 10, 0, 10
        //SB.SeekOn(new Vector3(10, 1, 10));

        SB.WanderOn();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
