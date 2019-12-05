using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public List<Edge> Neighbours = new List<Edge>();
    public float Cost = float.PositiveInfinity;
    public float Distance = 2;
    public int Index;
    public bool Walkable = true;
    Rigidbody rb;
    // Start is called before the first frame update
   public void RB()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
        rb.freezeRotation = true;
        gameObject.layer = 10;
        Walkable = true;        
    }

    public void GenerateNeighbours()
    {
        List<Node> hitObjects = new List<Node>();
        name = "Node: " + Index;
        var Scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z );
        foreach (var item in Physics.OverlapBox(transform.position + transform.forward * .5f, Scale * 0.5f, transform.rotation))
        {
            Node Temp = item.gameObject.GetComponent<Node>();
            if (item.transform.gameObject != gameObject && Temp != null)
                if (Temp.Walkable)
                    hitObjects.Add(Temp);
        }
        foreach (var item in hitObjects)
            Neighbours.Add(new Edge(this, item));
        Destroy(gameObject.GetComponent<Rigidbody>());

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x, 1, transform.localScale.z));


        Gizmos.color = Color.red;
        var Scale = new Vector3(transform.localScale.x , transform.localScale.y , transform.localScale.z );
        var Matrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward , transform.rotation, Scale *2);

        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix;
    }
    public abstract void Reset();
}
