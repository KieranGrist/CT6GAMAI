using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Blocked : TileNode
{
    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public  new void Start()
    {
        Walkable = false;
        GetComponent<Renderer>().material = MaterialManager.BlockedMat;
        if (!Created)
        {
            GameObject go = Instantiate(MaterialManager.BlockedGameObject, transform);
        go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        go.transform.localScale = new Vector3(1, 10, 1);
        CreatedObject = true;
        TileGameObject = go;
            Created = true;
        }
        Cost = int.MaxValue;
        name = "Blocked Tile. ID: " + Index;          
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public  new void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.BlockedMat;
        Cost = int.MaxValue;
        name = "Blocked Tile. ID: " + Index;
        if (NeedToReset)
        {
            Reset();
            NeedToReset = false;
        }

    }
}
