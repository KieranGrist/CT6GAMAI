using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TileGen : MonoBehaviour
{
    public GameObject Cube;
    public TileMaterials materials;
    public List<GameObject> GO = new List<GameObject>();
    public bool CubeGenreated;
    public int Area;
    int PreviousArea = 0;
    // Start is called before the first frame update
    void Start()
    {
        GenerateCube();
    }
    void GenerateCube()
    {
        foreach (var item in GO)
        {
            DestroyImmediate(item);
        }
        for (float x = transform.position.x  ; x < transform.position.x + (Area * 100); x += 100)
        {
            for (float z = transform.position.z  ; z < transform.position.z + (Area * 100); z += 100)
            {
                GameObject go = Instantiate(Cube, transform.position, transform.rotation);
                go.AddComponent<TileSelector>();
                go.GetComponent<TileSelector>().Map.MaterialManager = materials;
                go.transform.parent = GetComponent<Transform>();
                go.transform.position = new Vector3(x, 0, z);
                go.transform.localScale = new Vector3(100, 1, 100);
                GO.Add(go);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (CubeGenreated)
        {
            GenerateCube();
            CubeGenreated = false;
        }
        if (!Application.isPlaying && (Area != PreviousArea))
        {
            GenerateCube();
        }
        PreviousArea = Area;
    }
}
