using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Hills : TileNode
{
    public Color color;
    void Awake()
    {
        Distance = 1.25f;
        name = "Hills";
        color = new Color(34, 139, 34);
        GetComponent<Renderer>().sharedMaterial.color = new Color(34, 139, 34);
        Cost = 7;
        Reset();
    }
    public override void Start()
    {
        Cost = 7;
    }

    public override void Update()
    {
        color = new Color(34, 139, 34);
        GetComponent<Renderer>().sharedMaterial.color = new Color(34, 139, 34);
    }
}
