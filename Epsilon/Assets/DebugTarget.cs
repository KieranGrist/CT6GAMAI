using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
     
            foreach(var SB in FindObjectsOfType<SteeringBehaviours>())
        SB.SeekOn(new Vector2(transform.position.x, transform.position.z));
    }
}
