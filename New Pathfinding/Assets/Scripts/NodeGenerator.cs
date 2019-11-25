using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class NodeGenerator : MonoBehaviour
{
    public GameObject Cube;
    public List<GameObject> GO = new List<GameObject>();
    public float GapBetweenNodes = 1;
    float PreviousGapBetweenNodes = 0;
    public int Area;
    int PreviousArea = 0;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCube();
    }
    IEnumerator GenerateCube()
    {
        foreach (var item in GO)
            DestroyImmediate(item);

        for (float x = transform.position.x; x < transform.position.x + (Area * GapBetweenNodes); x += GapBetweenNodes)
            for (float z = transform.position.z; z < transform.position.z + (Area * GapBetweenNodes); z += GapBetweenNodes)

            {
                GameObject go = Instantiate(Cube, transform.position, transform.rotation);
                go.AddComponent<Node>();
                go.transform.parent = GetComponent<Transform>();
                go.transform.position = new Vector3(x, 0, z);
                go.transform.localScale = new Vector3(GapBetweenNodes, 0.01f, GapBetweenNodes);
                GO.Add(go);
            }
        foreach (var item in FindObjectsOfType<Node>())
            item.Reset();
        yield return new WaitForSeconds(0);

    }
    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying && ( Area != PreviousArea) ||GapBetweenNodes != PreviousGapBetweenNodes)
        {
          StartCoroutine(GenerateCube());
            PreviousArea = Area;
            PreviousGapBetweenNodes = GapBetweenNodes;
        }

    }
}
