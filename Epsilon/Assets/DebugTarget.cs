using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTarget : MonoBehaviour
{
  public  SteeringBehaviours SB;
    // Start is called before the first frame update
    void Start()
    {
        SteeringBehaviours SB =     FindObjectOfType<SteeringBehaviours>();
    }

    // Update is called once per frame
    void Update()
    {
        SB.SeekOn(new Vector2(transform.position.x, transform.position.z));
    }
}
