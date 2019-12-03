using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ProjCube : MonoBehaviour
{
   public List<GameObject> CollidedObjects;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

            CollidedObjects.Add(other.gameObject);

    }
    private void OnTriggerExit(Collider other)
    {

            CollidedObjects.Remove(other.gameObject);
    }
    public Vector3 CheckForAI(GameObject Other)
    {
        var distance = float.MaxValue;
        var ret = new Vector3();
        foreach(var item in CollidedObjects)
        {
            var d  = Vector3.Distance(Other.transform.position, item.transform.position);
            if (d < distance)
            {
                distance = d;
                ret = item.transform.position
            }

        }
        return new Vector3();
    }
}
