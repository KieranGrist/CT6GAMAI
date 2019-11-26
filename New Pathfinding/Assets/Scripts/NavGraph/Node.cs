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
        gameObject.layer = LayerMask.GetMask("Node");
        Walkable = true;        
    }
    // Update is called once per frame
    void Start()
    {
        name = "Node " + Index;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0.5F, 0), new Vector3(transform.localScale.x, 1, transform.localScale.z));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3((transform.localScale.x *2) - transform.localScale.x * 0.5f, 0.1f, (transform.localScale.z * 2) - transform.localScale.z *0.5f));
    }
    public abstract void Reset();
}
