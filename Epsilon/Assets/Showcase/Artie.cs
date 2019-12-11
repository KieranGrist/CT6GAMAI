using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Artie : MonoBehaviour
{
    //Values that effect the pathfinding of the AI 
     Node sourceNode;
     Node targetNode;
     Node previousSourceNode;
     Node previousTargetNode;
     GameObject Target;
    //Values that control the movement of the ai
     bool recievedPath = false;
     List<Transform> targetLocation = new List<Transform>();
     LayerMask mask;


    void Start()
    {
        mask = LayerMask.GetMask("Node");
        if (Physics.Raycast(RaceTrack.raceTrack.FinishLine.transform.position, -transform.up, out RaycastHit item,
            float.PositiveInfinity, mask))
            targetNode = item.transform.GetComponent<Node>();
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 20.0f, mask))
        {
            var Temp = hit.collider.gameObject.GetComponent<Node>();
            if (Temp)
                sourceNode = Temp;
        }

    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (targetLocation.Count > 0)
            foreach (var item in targetLocation)
                Gizmos.DrawWireCube(item.transform.position + new Vector3(0, 1, 0), new Vector3(4, 1, 4));
    }


    void GenerateRoute()
    {
        if (sourceNode && targetNode)
        {
            targetLocation.Clear();
            targetLocation = new List<Transform>();
            recievedPath = true;
            var Path = ASTAR.AStar.CalculatePath(sourceNode, targetNode);
            if (Path.Count > 0)
                for (int i = Path.Count - 1; i > 0; i--)
                {
                    targetLocation.Add(NavGraph.map.Nodes[Path[i]].transform);
                }
            targetLocation.Add(targetNode.transform);
        }


    }
    public virtual void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 20.0f, mask))
        {
            var Temp = hit.collider.gameObject.GetComponent<Node>();
            if (Temp)
                sourceNode = Temp;
        }
        if (Physics.Raycast(Target.transform.position + new Vector3(0, 20, 0), -transform.up, out RaycastHit item, float.PositiveInfinity, mask))
            targetNode = item.transform.GetComponent<Node>();
        if (previousSourceNode != sourceNode || targetNode != previousTargetNode)
            GenerateRoute();
        previousSourceNode = sourceNode;
        previousTargetNode = targetNode;

    }


}
