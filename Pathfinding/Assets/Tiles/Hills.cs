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
        name = "Hills Tile. ID: " + Index;
        color = new Color(34, 139, 34);
       
        Cost = 7;
        Reset();
    }
    public override void Start()
    {
        GetComponent<Renderer>().material = MaterialManager.HillsMat;
        Cost = 7;
    }

    public override void Update()
    {
        name = "Hills Tile. ID: " + Index;
        color = new Color(34, 139, 34);
        GetComponent<Renderer>().material = MaterialManager.HillsMat;
    }
}
