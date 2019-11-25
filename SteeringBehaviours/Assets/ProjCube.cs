using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ProjCube : MonoBehaviour
{
   public List<Collider> CollidedObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacles"))
            CollidedObjects.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacles"))
            CollidedObjects.Remove(other);
    }

}
