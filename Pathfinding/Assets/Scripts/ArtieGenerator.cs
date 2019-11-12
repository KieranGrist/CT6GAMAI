using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtieGenerator : MonoBehaviour
{
    public int ArtieAmmount = 15;
    public int XMin, XMax;
    public int ZMin, ZMax;
    public NavGraph Map;
 public GameObject Artie;
    public LayerMask Mask;
    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < ArtieAmmount; i++)
        {
            GameObject go = Instantiate(Artie, transform.position, transform.rotation);
            go.transform.position = new Vector3(Random.Range(XMin, XMax), 10, Random.Range(ZMin, ZMax));
            go.AddComponent<AIAgent>();
            go.GetComponent<AIAgent>().Map = Map;
            go.GetComponent<AIAgent>().Mask = Mask;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
