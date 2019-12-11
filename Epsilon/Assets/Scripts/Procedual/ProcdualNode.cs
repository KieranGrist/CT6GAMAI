using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcdualNode : Node
{
    public override void Reset()
    {
        Neighbours = new List<Edge>();
        Neighbours.Clear();
    }
}

