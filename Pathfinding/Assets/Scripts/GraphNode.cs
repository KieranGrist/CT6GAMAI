using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public int Index;
    public List<GraphEdge> AdjacencyList = new List<GraphEdge>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
       foreach(var item in AdjacencyList) {
            Gizmos.color = Color.blue;
            
            Gizmos.DrawLine(item.From.transform.position, item.To.transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
