using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Highway : TileNode
{
    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public  new void Start()
    {      
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;
        if (!Created)
        {
            GameObject go = Instantiate(MaterialManager.HighwayGameObject, transform);
        go.transform.position = new Vector3(transform.position.x, 0.01f     , transform.position.z);
        go.transform.localScale = new Vector3(1, 1, 1);
        CreatedObject = true;
        TileGameObject = go; 
            Created = true;
        }
        Cost = 2;
        name = "Highway Tile. ID: " + Index;          
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public  new void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.HighwayMat;
        Cost = 2;
        name = "Highway Tile. ID: " + Index;
        if (NeedToReset)
        {
            Reset();
            NeedToReset = false;
        }
    }
}