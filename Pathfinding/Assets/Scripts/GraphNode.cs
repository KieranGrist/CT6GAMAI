using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]

public class GraphNode : MonoBehaviour
{
    public int Index;
    public float Distance;

    public List<GraphEdge> AdjacencyList = new List<GraphEdge>();
    // Start is called before the first frame update

    void Start()
    {
        //CumilitiveCost += Distance;
        Reset();
    }
    public void Reset()
    {
        List<GraphNode> Nodes = new List<GraphNode>();
        Nodes.AddRange(FindObjectsOfType<GraphNode>());
        AdjacencyList.Clear();
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i] != this)
            {
                Distance = Vector3.Distance(transform.position, Nodes[i].transform.position);
                if (Vector3.Distance(transform.position, Nodes[i].transform.position) < 5)
                {
                    AdjacencyList.Add(new GraphEdge(this, Nodes[i]));
                }
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.position += new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }
    private void OnDrawGizmosSelected()
    {
       foreach(var item in AdjacencyList) {
            Gizmos.color = Color.blue;
            Vector3 TextLocation = new Vector3();
            TextLocation.x = item.From.transform.position.x + (item.To.transform.position.x - item.From.transform.position.x) / 2;
            TextLocation.y = item.From.transform.position.y + (item.To.transform.position.y - item.From.transform.position.y) / 2;
            TextLocation.z = item.From.transform.position.z + (item.To.transform.position.z - item.From.transform.position.z) / 2;

            Handles.Label(TextLocation, "Cost " + Vector3.Distance(item.From.transform.position, item.To.transform.position));
            Gizmos.DrawLine(item.From.transform.position, item.To.transform.position);
        }
    }
    public IEnumerator CheckForUpdates()
    {
        Reset();
       yield return new WaitForSeconds(30);
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckForUpdates());
    }
}
