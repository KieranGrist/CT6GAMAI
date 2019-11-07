using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
[ExecuteInEditMode]
public enum TileType
{
    Airport,
    Blocked,
    Bridge,
    Empty,
    Farm,
    Fence,
    Forrest,
    Grass,
    Highway,
    Hills,
    Jungle,
    Lake,
    Lava,
    MilitaryAirport,
    MilitaryBase,
    Mines,
    Ocean,
    Path,
    Power,
    Residential,
    River,
    Road,
    Rocks,
    Snow,
    SpacePort,
    Storage,
    TrainStation,
    TrainTracks
}

public class TileSelector : MonoBehaviour
{
    public TileMaterials MaterialManager;

    public TileType type;
    TileType PreviousType;
    // Update is called once per frame
    void TileCreator()
    {
     
        {
          
            if (type != PreviousType)
            {
                foreach (var item in gameObject.GetComponents<TileNode>())
                {
                    DestroyImmediate(item);
                }

                switch (type)
                {
                    default:
                        gameObject.AddComponent<Empty>();
                        break;
                    case (TileType.Airport):
                        gameObject.AddComponent<Airport>();
                        break;
                    case (TileType.Blocked):
                        gameObject.AddComponent<Blocked>();
                        break;
                    case (TileType.Bridge):
                        gameObject.AddComponent<Bridge>();
                        break;
                    case (TileType.Empty):
                        gameObject.AddComponent<Empty>();
                        break;
                    case (TileType.Farm):
                        gameObject.AddComponent<Farm>();
                        break;
                    case (TileType.Fence):
                        gameObject.AddComponent<Fence>();
                        break;
                    case (TileType.Forrest):
                        gameObject.AddComponent<Forrest>();
                        break;
                    case (TileType.Grass):
                        gameObject.AddComponent<Grass>();
                        break;
                    case (TileType.Highway):
                        gameObject.AddComponent<Highway>();
                        break;
                    case (TileType.Hills):
                        gameObject.AddComponent<Hills>();
                        break;
                    case (TileType.Jungle):
                        gameObject.AddComponent<Jungle>();
                        break;
                    case (TileType.Lake):
                        gameObject.AddComponent<Lake>();
                        break;
                    case (TileType.Lava):
                        gameObject.AddComponent<Lava>();
                        break;
                    case (TileType.MilitaryAirport):
                        gameObject.AddComponent<MilitaryAirport>();
                        break;
                    case (TileType.MilitaryBase):
                        gameObject.AddComponent<MilitaryBase>();
                        break;
                    case (TileType.Mines):
                        gameObject.AddComponent<Mines>();
                        break;
                    case (TileType.Ocean):
                        gameObject.AddComponent<Ocean>();
                        break;
                    case (TileType.Path):
                        gameObject.AddComponent<Path>();
                        break;
                    case (TileType.Power):
                        gameObject.AddComponent<Power>();
                        break;
                    case (TileType.Residential):
                        gameObject.AddComponent<Residential>();
                        break;
                    case (TileType.River):
                        gameObject.AddComponent<River>();
                        break;
                    case (TileType.Road):
                        gameObject.AddComponent<Road>();
                        break;
                    case (TileType.Rocks):
                        gameObject.AddComponent<Rocks>();
                        break;
                    case (TileType.Snow):
                        gameObject.AddComponent<Snow>();
                        break;
                    case (TileType.SpacePort):
                        gameObject.AddComponent<SpacePort>();
                        break;
                    case (TileType.Storage):
                        gameObject.AddComponent<Storage>();
                        break;
          
                    case (TileType.TrainStation):
                        gameObject.AddComponent<TrainStation>();
                        break;
                    case (TileType.TrainTracks):
                        gameObject.AddComponent<TrainTracks>();
                        break;                  
                }
 
                PreviousType = type;             
       
                gameObject.GetComponent<TileNode>().MaterialManager = MaterialManager;
                gameObject.GetComponent<TileNode>().enabled = true;            
            }
        }
    }  
    void Start()
    {
        TileCreator();
    }
    void Update()
    {
        TileCreator();
    }
}