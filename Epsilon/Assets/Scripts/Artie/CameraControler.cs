using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public GameObject AI; //AI Object to follow 
 
   void Update()
    {
        if (AI ==null && FindObjectOfType<AIAgent>()) //if AI hasnt been assigned and it can find an AI
            AI = FindObjectOfType<AIAgent>().transform.gameObject; //Focus on AI agent found 
        transform.parent = AI.transform; //Set the parent of the camera to be that of the AI
        transform.localPosition = new Vector3(0, 5, -5); //Force the camera to be in a third view perspective
    }

}
