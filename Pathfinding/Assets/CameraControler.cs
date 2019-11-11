﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(1, 0, 0);
        if (Input.GetKey(KeyCode.S))
            transform.position -= new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(1, 0, 0);
        if (Input.GetKey(KeyCode.Q))    
            transform.position += new Vector3(0, 1, 0);    
        if (Input.GetKey(KeyCode.Z))
            transform.position -= new Vector3(0, 1, 0);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(new Vector3(1, 0, 0), 1);
        if (Input.GetKey(KeyCode.C))
            transform.Rotate(new Vector3(-1, 0, 0), 1);
        if (Input.GetKey(KeyCode.R))
        {
         


        }
    }
}
