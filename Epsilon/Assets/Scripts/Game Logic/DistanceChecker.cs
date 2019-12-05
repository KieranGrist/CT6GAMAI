using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DistanceChecker : MonoBehaviour
{
    public float Distance;
    public float Dot;
    public GameObject Node, Artie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(Node.transform.position, Artie.transform.position);
        Vector3 forward = Node.transform.forward;
        Vector3 toOther = Artie.transform.position - Node.transform.position;

        //Vector3 forward = transform.forward;
        //Vector3 toOther = Node.transform.position - Artie.transform.position;
        Dot =Vector3.Dot(forward, toOther);
    }
}
