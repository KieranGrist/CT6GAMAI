using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArtieGenerator : MonoBehaviour
{
    public int ArtieAmmount = 15;
    public float XMin, XMax;
    public float ZMin, ZMax;
    public GameObject Artie;
    public bool Generated;
    public LayerMask Mask;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Application.isPlaying)
        {
            if (!Generated)
            {
                XMin = float.MaxValue;
                XMax = float.MinValue;
                ZMax = float.MinValue;
                ZMin = float.MaxValue;
                foreach (var item in NavGraph.map.Nodes)
                {
                    if (item.transform.position.x < XMin)
                        XMin = item.transform.position.x;
                    if (item.transform.position.x > XMax)
                        XMax = item.transform.position.x;
                    if (item.transform.position.z < ZMin)
                        ZMin = item.transform.position.z;
                    if (item.transform.position.z > ZMax)
                        ZMax = item.transform.position.x;
                }
            }
            if (!Generated && XMin != float.MaxValue)
            {
                for (int i = 0; i < ArtieAmmount; i++)
                {
                    GameObject go = Instantiate(Artie, transform.position, transform.rotation);
                    go.transform.position = new Vector3(Random.Range(XMin, XMax), 10, Random.Range(ZMin, ZMax));
                    go.AddComponent<AIAgent>();
                    go.GetComponent<AIAgent>().Mask = Mask;
                }
                Generated = true;
            }
        }
    }
}
