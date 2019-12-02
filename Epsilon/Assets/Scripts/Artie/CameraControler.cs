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
        if (AI ==null && FindObjectOfType<AIAgent>())
            AI = FindObjectOfType<AIAgent>().transform.gameObject;
        transform.parent = AI.transform;
        transform.localPosition = new Vector3(0, 5, -5);


    }

}
