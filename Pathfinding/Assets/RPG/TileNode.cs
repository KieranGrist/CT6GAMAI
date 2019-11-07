using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public abstract class TileNode : MonoBehaviour
{
    public TileMaterials MaterialManager;
    public int Cost = int.MaxValue;
    public float Distance = 1.25f;
    public int Index;
    public bool Walkable;
    public List<TileEdge> Neighbours = new List<TileEdge>();
    public TileNode()
    {
        
        Index = 0;
        Walkable = true;
        Neighbours = new List<TileEdge>();
    }
 
    public TileNode(int Index, bool Walkable)
    {
        this.Index = Index;
        this.Walkable = Walkable;
        Neighbours = new List<TileEdge>();
    }
    public abstract void Start();
    public abstract void Update();
    public void Reset()
    {
        List<TileNode> Nodes = new List<TileNode>();
        Neighbours.Clear();
        Nodes.AddRange(FindObjectsOfType<TileNode>());
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Distance);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (Nodes[i] != this)
                Neighbours.Add(new TileEdge(GetComponent<TileNode>(), hitColliders[i].gameObject.GetComponent<TileNode>()));
            i++;
        }
    }

    private void OnDrawGizmosSelected()
    {
    if (Application.isPlaying)
        foreach (var item in Neighbours)
        {
            Vector3 TextLocation = new Vector3();
            TextLocation.x = item.From.transform.position.x + (item.To.transform.position.x - item.From.transform.position.x) / 2;
            TextLocation.y = item.From.transform.position.y + (item.To.transform.position.y - item.From.transform.position.y) / 2;
            TextLocation.z = item.From.transform.position.z + (item.To.transform.position.z - item.From.transform.position.z) / 2;

            Handles.Label(TextLocation, "Cost " + item.To.GetComponent<TileNode>().Cost);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(item.From.transform.position, item.To.transform.position);
        }
    }
    
}
