using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[ExecuteInEditMode]
public class Forrest : TileNode
{
    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public  new void Start()
    {

        if (!Created)
        {
            GetComponent<Renderer>().material = MaterialManager.ForrestMat;
        GameObject go = Instantiate(MaterialManager.ForrestGameObject, transform);
        go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        go.transform.localScale = new Vector3(1, 1, 1);
        CreatedObject = true;
        TileGameObject = go;
            Created = true;
        }
        Cost = 20;
        name = "Forrest Tile. ID: " + Index;          
        foreach (var item in Neighbours)
        {
            item.From = GetComponent<TileNode>();
        }
    }


    public  new void Update()
    {
        GetComponent<Renderer>().material = MaterialManager.ForrestMat;
        Cost = 20;
        name = "Forrest Tile. ID: " + Index;
        if (NeedToReset)
        {
            Reset();
            NeedToReset = false;
        }

    }
}
