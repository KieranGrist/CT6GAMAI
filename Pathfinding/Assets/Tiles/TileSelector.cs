﻿using System.Collections;
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
    TankDepot,
    TrainStation,
    TrainTracks
}
[ExecuteInEditMode]
public class TileSelector : MonoBehaviour
{
    public TileMaterials tileMaterials;

    public TileType type = TileType.Empty;
    TileType PreviousType;
    // Update is called once per frame
    void TileCreator()
    {
        if (!Application.isPlaying)
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
                        gameObject.AddComponent<Hills>();
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
                    case (TileType.TankDepot):
                        gameObject.AddComponent<TankDepot>();
                        break;
                    case (TileType.TrainStation):
                        gameObject.AddComponent<TrainStation>();
                        break;
                    case (TileType.TrainTracks):
                        gameObject.AddComponent<TrainTracks>();
                        break;                  
                }
                gameObject.GetComponent<TileNode>().Reset();
                PreviousType = type;
                gameObject.GetComponent<TileNode>().MaterialManager = tileMaterials;
                gameObject.GetComponent<TileNode>().enabled = true;
            }
        }
    }
    void Awake()
    {
        TileCreator();
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