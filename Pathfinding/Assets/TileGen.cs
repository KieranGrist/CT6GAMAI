using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGen : MonoBehaviour
{
    public GameObject Cube;
    int Area = 15;
    // Start is called before the first frame update
    void Start()
    {
        for (float x = transform.position.x - Area; x < transform.position.x + Area; x++)
        {
            for (float z = transform.position.z - Area; z < transform.position.z + Area; z++)
            {
                GameObject go = Instantiate(Cube, transform.position, transform.rotation);
                go.AddComponent<TileNode>();
                go.transform.parent = GetComponent<Transform>();
                go.transform.position = new Vector3(x, 2, z);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
