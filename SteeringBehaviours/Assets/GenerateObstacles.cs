using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    public List<GameObject> Models = new List<GameObject>();
    public List<GameObject> GenreatedObjects = new List<GameObject>();
    public float Radius;
    public float CluterAmmount = 500;
    float CluterAmmountLast = 0;
    // Update is called once per frame
    void Update()
    { 
        
        if (CluterAmmount != CluterAmmountLast)
        {
                    foreach (var item in GenreatedObjects)
                        Destroy(item);
            GenreatedObjects.Clear();
            for (int i = 0; i < CluterAmmount; i++)
            {       
                GameObject go = Instantiate(Models[Random.Range(0, Models.Count)], transform.position, transform.rotation);
                go.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
                go.transform.position.Normalize();
                go.transform.position *= Radius;
                go.transform.position += new Vector3(transform.position.x, 1, transform.position.z);
                go.transform.parent = transform;
                go.AddComponent<Rigidbody>();
                go.tag = "Obstacles";
                GenreatedObjects.Add(go);
            }
        }
        CluterAmmountLast = CluterAmmount;

    }
}
