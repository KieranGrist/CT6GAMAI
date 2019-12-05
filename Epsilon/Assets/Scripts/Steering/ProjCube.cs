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
    /// <summary>
    /// Returns the closet ai within the projection cube
    /// </summary>
    /// <param name="Other"></param>
    /// <returns></returns>
    public Vehicle CheckForAI(Vehicle Other)
    {
        var distance = float.MaxValue;
        Vehicle ret = Other;
        ret = null;
        foreach(var item in CollidedObjects)
        {
            var Ref = item.GetComponent<Vehicle>();
            if (Ref)
            {
                var d = Vector3.Distance(Other.transform.position, item.transform.position);

                if (d < distance)
                {
                    distance = d;
                    ret = Ref;
                }
            }
        }
        return ret;
    }

}
