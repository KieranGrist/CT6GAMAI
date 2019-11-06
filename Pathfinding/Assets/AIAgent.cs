using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public List<GameObject> PathGameObjects = new List<GameObject>();
    public NavGraph navGraph;
    float Distance;
    public bool RecievedPath = false;
    public bool Moving = false;
    public List<Vector3> TargetLocation = new List<Vector3>();
    TileNode PreviousNode;
    public List<float> Speed = new List<float>();
    Vector3 Direction;
    int PreviousNodeID = int.MaxValue;
    Vector3 Normalise;
    Vector3 M;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            PreviousNode = navGraph.SourceNode;
            if (hit.transform.gameObject.GetComponent<TileNode>())
            {
                if (hit.transform.gameObject.GetComponent<TileNode>().Index != PreviousNodeID)
                {
                    PreviousNodeID = hit.transform.gameObject.GetComponent<TileNode>().Index;
                    GameObject GO = Instantiate(navGraph.Cube, transform.position, transform.rotation);
                    GO.transform.position = PreviousNode.transform.position;   
                    GO.transform.position += new Vector3(0, 0.1f, 0);
                    GO.transform.localScale = new Vector3(1, 1, 1);
                    GO.name = "Path Node";
                    PathGameObjects.Add(GO);
                    navGraph.SourceNode = hit.transform.gameObject.GetComponent<TileNode>();
                }
            }
        }
        Distance = Vector3.Distance(navGraph.SourceNode.transform.position, transform.position);

        if (navGraph.FoundRoute && !RecievedPath)
        {
            foreach (var item in PathGameObjects)
            {
                Destroy(item);
            }
            TargetLocation.Clear();
            Speed.Clear();
            RecievedPath = true;
            for (int i=navGraph.PathfindingTechnique.GeneratedPath.Count -1; i > 0; i-- )
            {
                TargetLocation.Add(navGraph.Nodes[navGraph.PathfindingTechnique.GeneratedPath[i]].transform.position);
                Speed.Add(5 / (float)navGraph.Nodes[navGraph.PathfindingTechnique.GeneratedPath[i]].Cost);
            }
            TargetLocation.Add(navGraph.TargetNode.transform.position);
            Speed.Add(5);
        }
        if (TargetLocation.Count > 0)
        {
            if (TargetLocation[0] != null)
            {
                Distance = Vector3.Distance(TargetLocation[0], transform.position);

                if (Distance <= 0.05f)
                {
                    Moving = false;
                    TargetLocation.Remove(TargetLocation[0]);
                    Speed.Remove(Speed[0]);
                }
                else
                {
                    Moving = true;
                }

            }   
        }
        if (Moving == true)
        {
            TargetLocation[0] = new Vector3(TargetLocation[0].x, 4, TargetLocation[0].z);
            // direction, normalise, direction * DeltaTime and speed, add that to current location
            Direction = TargetLocation[0] - transform.position;
            Normalise = Direction.normalized;
            M = Normalise * Time.deltaTime * Speed[0];
            transform.position += M;
        }
    }
}
