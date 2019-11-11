using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
[ExecuteInEditMode]
public abstract class TileNode : MonoBehaviour
{
    public Vector3 GameObjectRotation;
    public bool NeedToReset = false;
    public TileMaterials MaterialManager;
    public GameObject TileGameObject;
    public List<GameObject> gameObjects = new List<GameObject>();
    public bool CreatedObject = false;
    public int Cost = int.MaxValue;
    public float Distance = 60;
    public int Index;
    public bool Walkable;
    public bool Created;
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
    public void Start()
    {

    }
    public void Update()
    {   

  
    }
    public abstract void Reset();

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Distance);
        foreach (var item in Neighbours)
        {
            Vector3 TextLocation = new Vector3();
            TextLocation.x = item.From.transform.position.x + (item.To.transform.position.x - item.From.transform.position.x) / 2;
            TextLocation.y = item.From.transform.position.y + (item.To.transform.position.y - item.From.transform.position.y) / 2;
            TextLocation.z = item.From.transform.position.z + (item.To.transform.position.z - item.From.transform.position.z) / 2;

            Handles.Label(TextLocation, "Cost " + item.To.GetComponent<TileNode>().Cost);
            Gizmos.color = Color.blue;
            Vector3 A, B;
            A = item.From.transform.position;
            A += new Vector3(0, 1, 0);
            B = item.To.transform.position;
            B += new Vector3(0, 1, 0);
            Gizmos.DrawLine(A,B);
        }
    }
    
}
