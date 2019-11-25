using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    float Speed;
    public GameObject AI;
    public bool ToggleFocus;
    // Start is called before the first frame update

    // Update is called once per frame
   void Update()
    {
        if (AI ==null)
            AI = FindObjectOfType<AIAgent>().transform.gameObject;
        if (Input.GetKey(KeyCode.LeftShift))
            Speed = 15;
        else
            Speed = 7.5f;
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, 0, Speed);
        if (Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(Speed, 0, 0);
        if (Input.GetKey(KeyCode.S))
            transform.position -= new Vector3(0, 0, Speed);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(Speed, 0, 0);
        if (Input.GetKey(KeyCode.Q))    
            transform.position += new Vector3(0, Speed, 0);    
        if (Input.GetKey(KeyCode.Z))
            transform.position -= new Vector3(0, Speed, 0);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(new Vector3(1, 0, 0), 1);
        if (Input.GetKey(KeyCode.C))
            transform.Rotate(new Vector3(-1, 0, 0), 1);
        if (Input.GetKey(KeyCode.R))
        {
            transform.eulerAngles = new Vector3(90, 0, 0);
            transform.position = new Vector3(transform.position.x, 20, transform.position.z);
        }
        if (Input.GetKey(KeyCode.F))
            ToggleFocus = !ToggleFocus;
        if (ToggleFocus)
            transform.position = new Vector3(AI.transform.position.x, 20, AI.transform.position.z);


    }

}
