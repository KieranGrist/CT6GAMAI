using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNav : MonoBehaviour
{
    public GameObject Node;
    public NavGraph graph;
    public MasterTarget target;
    public int Area;
    int PreviousArea = -15000;
    public List<GameObject> gameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Area != PreviousArea)
        {
            for (int i =0; i < gameObjects.Count; i ++)
            {
                Destroy(gameObjects[i]);
            }
            gameObjects.Clear();
            for (int x = 0 - Area; x < Area; x+=2)
            {
                for (int z = 0 - Area; z < Area; z+=2)
                {
                    GameObject GO = Instantiate(Node);
                    GO.transform.position = new Vector3(x, 3, z);
                    gameObjects.Add(GO);
                }
            }
            List<GraphNode> nodes = new List<GraphNode>();
            nodes.AddRange(FindObjectsOfType<GraphNode>());
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Reset();
            }
            target.Target = gameObjects[0].GetComponent<GraphNode>();
            target.Source = gameObjects[1].GetComponent<GraphNode>();
            graph.Reset();
            target.Reset();
        }
        PreviousArea = Area;
    }
}
