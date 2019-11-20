using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ProjCube : MonoBehaviour
{
   public List<GameObject> CollidedObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Obstacles"))
            CollidedObjects.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacles"))
            CollidedObjects.Remove(other.gameObject);
    }
    // Update is called once per frame
    void LateUpdate()
    {
      //  CollidedObjects.Clear();
    }
}
