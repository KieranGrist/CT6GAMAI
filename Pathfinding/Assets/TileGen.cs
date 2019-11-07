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
    public int Area = 3;
    int PreviousArea = 0;
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }
    void CreateGrid()
    {
    //    List<TileNode> ListOfNodes = new List<TileNode>(); ListOfNodes.AddRange(FindObjectsOfType<TileNode>());
    //    GO.Clear();
    //    foreach (var item in ListOfNodes)
    //        GO.Add(item.transform.gameObject);
        foreach (var item in GO)
        {
            Destroy(item);
        }
        GO.Clear();
        for (float x = transform.position.x - (Area  ); x < transform.position.x + (Area ); x +=100)
        {
            for (float z = transform.position.z - (Area ); z < transform.position.z + (Area ); z += 100)
            {

                GameObject go = Instantiate(Cube, transform.position, transform.rotation);
           //     go.AddComponent<TileSelector>();
             //   go.GetComponent<TileSelector>().MaterialManager = materials;
                go.transform.localScale = new Vector3(100, 0.5f, 100);
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
            CreateGrid();
            GenerateCube = false;
        }
        if (!Application.isPlaying && (Area != PreviousArea))
        {
            CreateGrid();

        }
        PreviousArea = Area;
    }
}
