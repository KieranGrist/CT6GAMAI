using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garage : MonoBehaviour
{
    private void Update()
    {
      foreach( var item in Physics.OverlapSphere(transform.position,20))
            if (item.CompareTag("AI"))
            {
                var vic = item.GetComponent<Vehicle>();
            
            }

    }



}
