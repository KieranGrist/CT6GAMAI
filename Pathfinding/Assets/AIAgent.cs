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
    public float InitialSpeed = 25.0f;
    public LayerMask Mask;
    public List<float> Speed = new List<float>();
    Vector3 Direction;
    int PreviousNodeID = int.MaxValue;
    Vector3 Normalise;
    Vector3 M;
    void Start()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mask))
        {
            PreviousNode = navGraph.SourceNode;
            if (hit.transform.gameObject.GetComponent<TileNode>())
                if (hit.transform.gameObject.GetComponent<TileNode>().Index != PreviousNodeID)
                    navGraph.SourceNode = hit.transform.gameObject.GetComponent<TileNode>();
        }
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mask))
        {
            PreviousNode = navGraph.SourceNode;
            if (hit.transform.gameObject.GetComponent<TileNode>())
                if (hit.transform.gameObject.GetComponent<TileNode>().Index != PreviousNodeID)
                    navGraph.SourceNode = hit.transform.gameObject.GetComponent<TileNode>();
        }

        Distance = Vector3.Distance(navGraph.SourceNode.transform.position, transform.position);

        if (navGraph.FoundRoute && !RecievedPath)
        {
            foreach (var item in PathGameObjects)   
                Destroy(item);
  
            TargetLocation.Clear();
            Speed.Clear();
            RecievedPath = true;
            for (int i=navGraph.PathfindingTechnique.GeneratedPath.Count -1; i > 0; i-- )
            {
                TargetLocation.Add(navGraph.Nodes[navGraph.PathfindingTechnique.GeneratedPath[i]].transform.position);
                Speed.Add(InitialSpeed / (float)navGraph.Nodes[navGraph.PathfindingTechnique.GeneratedPath[i]].Cost);
            }
            TargetLocation.Add(navGraph.TargetNode.transform.position);
            Speed.Add(InitialSpeed);
        }
        if (TargetLocation.Count > 0)
        {
            if (TargetLocation[0] != null)
            {
                Distance = Vector3.Distance(TargetLocation[0], transform.position);

                if (Distance <= 0.5f)
                {
                    transform.position = TargetLocation[0];
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
            TargetLocation[0] = new Vector3(TargetLocation[0].x, 10, TargetLocation[0].z);
            Direction = TargetLocation[0] - transform.position;
            Normalise = Direction.normalized;
            M = Normalise * Time.deltaTime * Speed[0]; 
            transform.position += M;
        }
    }
}
