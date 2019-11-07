using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TileGen : MonoBehaviour
{
    public GameObject Cube;
    public TileMaterials materials;
    public List<GameObject> GO = new List<GameObject>();
    public bool GenerateCube;
    public int Area;
    int PreviousArea = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in GO)
        {
            Destroy(item);
        }
        for (float x = transform.position.x - Area; x < transform.position.x + Area; x++)
        {
            for (float z = transform.position.z - Area; z < transform.position.z + Area; z++)
            {
                GameObject go = Instantiate(Cube, transform.position, transform.rotation);
                go.AddComponent<TileSelector>();
                go.transform.parent = GetComponent<Transform>();
                go.transform.position = new Vector3(x, 2, z);
                GO.Add(go);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GenerateCube)
        {
            foreach (var item in GO)
            {
                Destroy(item);
            }
            for (float x = transform.position.x - Area; x < transform.position.x + Area; x++)
            {
                for (float z = transform.position.z - Area; z < transform.position.z + Area; z++)
                {
                    GameObject go = Instantiate(Cube, transform.position, transform.rotation);
                    go.AddComponent<TileSelector>();
                    go.GetComponent<TileSelector>().tileMaterials = materials;
                    go.transform.parent = GetComponent<Transform>();
                    go.transform.position = new Vector3(x, 2, z);
                    GO.Add(go);
                }
            }
            GenerateCube = false;
        }
        if (!Application.isPlaying && (Area != PreviousArea))
        {
            foreach (var item in GO)
            {
                Destroy(item);
            }
            for (float x = transform.position.x - Area; x < transform.position.x + Area; x++)
            {
                for (float z = transform.position.z - Area; z < transform.position.z + Area; z++)
                {
                    GameObject go = Instantiate(Cube, transform.position, transform.rotation);
                    go.AddComponent<TileSelector>();
                    go.transform.parent = GetComponent<Transform>();
                    go.transform.position = new Vector3(x, 2, z);
                    GO.Add(go);
                }
            }

        }
        PreviousArea = Area;
    }
}
