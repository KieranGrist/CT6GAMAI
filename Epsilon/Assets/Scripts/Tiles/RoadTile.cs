using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : Node
{
    private void Update()
    {
        gameObject.layer = 10;
    }

    public override void Reset()
    {
        Vector3 Scale = new Vector3();
        Vector3 P = transform.position;
        gameObject.AddComponent<Rigidbody>();
        Scale = new Vector3(transform.localScale.x * 0.5f, 1, transform.localScale.z * 0.5f);
        RB();


        Walkable = true;
        if (Physics.BoxCast(transform.position - new Vector3(0, 20, 0), Scale, transform.up, out RaycastHit hit, transform.rotation, float.PositiveInfinity, LayerMask.GetMask("Road")))
            transform.rotation = hit.transform.GetComponent<Road>().transform.rotation;
        else
            Walkable = false;
        if (Physics.BoxCast(transform.position - new Vector3(0, 20, 0), Scale, transform.up, transform.rotation, float.PositiveInfinity, LayerMask.GetMask("Obstacle", "Walls")))
            Walkable = false;
        Neighbours = new List<Edge>();
        Neighbours.Clear();
        transform.position = P;
        Destroy(GetComponent<Rigidbody>());
        if (!Walkable)
            Destroy(this.gameObject);

    }

}
