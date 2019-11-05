using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
/*
BLUE = WATER = 10
GREEN = GRASS = 4 
DARK GREEN = HILLS = 7
WHITE = SNOW = 9
RED = UNPASSABLE TILE = infinity
Orange = Main  Road = 2 
Light Orange = Bridge = 4
Pink = Residential = 6

Source = Purple
Destination = Red

Route = Transparent black 
Path = Transparent Yellow
*/
public abstract class TileNode : MonoBehaviour
{
    public TileMaterials MaterialManager;
    public float Cost = float.MaxValue;
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

        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i] != this)
            {
                
                if (Vector3.Distance(transform.position, Nodes[i].transform.position) < Distance)
                {
                    Neighbours.Add(new TileEdge(this, Nodes[i]));
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
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
