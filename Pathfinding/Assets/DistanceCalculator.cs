using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DistanceCalculator : MonoBehaviour
{
    public float Distance;
    public GameObject Target, Source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(Target.transform.position, Source.transform.position);
    }
}
