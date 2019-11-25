using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRaycast : MonoBehaviour
{
    public Vector3 Normal;
    public float WallAvodidenceDistance = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Norm = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        LayerMask mask = LayerMask.GetMask("Walls");
        RaycastHit hit;
        var Feelers = new[]
{
                  transform.position + (Norm * WallAvodidenceDistance),
                  transform.position + Quaternion.AngleAxis(45, Vector3.up) * Norm * (WallAvodidenceDistance / 2),
                  transform.position + Quaternion.AngleAxis(-45, Vector3.up) * Norm * (WallAvodidenceDistance / 2)
        };
        foreach (var item in Feelers)
        {
            if (Physics.Raycast(transform.position, item - transform.position, out hit, item.magnitude, mask))
            {
                Debug.DrawRay(transform.position, item - transform.position, Color.red);
                Normal = hit.normal;
            }
            else
            {
                Debug.DrawRay(transform.position, item - transform.position, Color.blue);
            }
        }
    }
}
