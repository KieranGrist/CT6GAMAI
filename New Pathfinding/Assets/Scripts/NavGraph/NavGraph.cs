﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavGraph : MonoBehaviour
{
    public List<Node> Nodes = new List<Node>();
    public ASTAR PathfindingTechnique;
    LayerMask TileMask;
    public static NavGraph map;
    public bool ResetMap;
    void Awake()
    {
        AddNodes();
        map = this;
        DontDestroyOnLoad(gameObject);
        ResetAllNodes();
    }
    void AddNodes()
    {
        Nodes.Clear();
        Nodes.AddRange(FindObjectsOfType<Node>());
        for (int i = 0; i < Nodes.Count; i++)
            Nodes[i].Index = i;
    }

    // Update is called once per frame
    void Update()
    {
        map = this;
        if (ResetMap) { ResetAllNodes();  ResetMap = false; }

    }

    public void ResetAllNodes()
    {
        AddNodes();
        foreach (var item in Nodes)
            item.Reset();
    }
}