using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Snow : TileNode
{
    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public  new void Start()
    {
       
        GetComponent<Renderer>().material = MaterialManager.SnowMat;
        if (!Created)
        {
            GameObject go = Instantiate(MaterialManager.EmptyGameObject, transform);
            go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            go.transform.localScale = new Vector3(1, 1, 1);
            CreatedObject = true;
            TileGameObject = go;
            Created = true;
        }
        Cost = 30;
        name = "Snow Tile. ID: " + Index;          
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public  new void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.SnowMat;
        Cost = 30;
        name = "Snow Tile. ID: " + Index;
        if (NeedToReset)
        {
            Reset();
            NeedToReset = false;
        }
    }
}
