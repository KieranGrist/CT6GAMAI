using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
[ExecuteInEditMode]
public enum TileType
{
    Blocked,
    Bridge,
    Empty,
    Grass,
    Highway,
    Hills,
    Residential,
    Snow,
    Water
}
[ExecuteInEditMode]
public class TileSelector : MonoBehaviour
{
    public TileMaterials tileMaterials;

    public TileType type = TileType.Empty;
    TileType PreviousType;
    // Update is called once per frame
    void Awake()
    {
        if (!Application.isPlaying)
        {
            gameObject.GetComponent<TileNode>().MaterialManager = tileMaterials;
            gameObject.GetComponent<TileNode>().enabled = true;
            if (type != PreviousType)
            {
                foreach (var item in gameObject.GetComponents<TileNode>())
                {
                    DestroyImmediate(item);
                }
     
                switch (type)
                {
                    case TileType.Blocked:
                        gameObject.AddComponent<Blocked>();            
 
                        gameObject.GetComponent<Blocked>().Reset();
                        break;
                    case TileType.Bridge:
                        gameObject.AddComponent<Bridge>();
                        gameObject.GetComponent<Bridge>().Reset();
                        break;
                    case TileType.Empty:
                        gameObject.AddComponent<Empty>();
                        gameObject.GetComponent<Empty>().Reset();
                        break;
                    case TileType.Grass:
                        gameObject.AddComponent<Grass>();
                        gameObject.GetComponent<Grass>().Reset();
                        break;
                    case TileType.Highway:
                        gameObject.AddComponent<Highway>();
                        gameObject.GetComponent<Highway>().Reset();
                        break;
                    case TileType.Hills:
                        gameObject.AddComponent<Hills>();
                        gameObject.GetComponent<Hills>().Reset();
                        break;
                    case TileType.Residential:
                        gameObject.AddComponent<Residential>();       
                        gameObject.GetComponent<Residential>().Reset();
                        break;
                    case TileType.Snow:
                        gameObject.AddComponent<Snow>();          
                        gameObject.GetComponent<Snow>().Reset();
                        break;
                    case TileType.Water:
                        gameObject.AddComponent<Water>();
                        gameObject.GetComponent<Water>().Reset();
                        break;

                }
                PreviousType = type;

            }
        }
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            gameObject.GetComponent<TileNode>().MaterialManager = tileMaterials;
            gameObject.GetComponent<TileNode>().enabled = true;
            if (type != PreviousType)
            {
                foreach (var item in gameObject.GetComponents<TileNode>())
                {
                    DestroyImmediate(item);
                }

                switch (type)
                {
                    case TileType.Blocked:
                        gameObject.AddComponent<Blocked>();

                        gameObject.GetComponent<Blocked>().Reset();
                        break;
                    case TileType.Bridge:
                        gameObject.AddComponent<Bridge>();
                        gameObject.GetComponent<Bridge>().Reset();
                        break;
                    case TileType.Empty:
                        gameObject.AddComponent<Empty>();
                        gameObject.GetComponent<Empty>().Reset();
                        break;
                    case TileType.Grass:
                        gameObject.AddComponent<Grass>();
                        gameObject.GetComponent<Grass>().Reset();
                        break;
                    case TileType.Highway:
                        gameObject.AddComponent<Highway>();
                        gameObject.GetComponent<Highway>().Reset();
                        break;
                    case TileType.Hills:
                        gameObject.AddComponent<Hills>();
                        gameObject.GetComponent<Hills>().Reset();
                        break;
                    case TileType.Residential:
                        gameObject.AddComponent<Residential>();
                        gameObject.GetComponent<Residential>().Reset();
                        break;
                    case TileType.Snow:
                        gameObject.AddComponent<Snow>();
                        gameObject.GetComponent<Snow>().Reset();
                        break;
                    case TileType.Water:
                        gameObject.AddComponent<Water>();
                        gameObject.GetComponent<Water>().Reset();
                        break;

                }
                PreviousType = type;
            }
        }
    }
}