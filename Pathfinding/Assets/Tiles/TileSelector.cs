using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]

/*Game objects from:
Trees from: RAKSHI GAMES Realistic Tree 9 [Rainbow Tree]

Materials from:
 Grass, Dirt, Rocks: JOHN'S JUNKYARD ASSETS
PBR Ground Materials #2 [Dirt, Grass & Rocky]
Grass:  ALP8310
Grass Flowers Pack Free
*/
public enum TileType
{
    Airport,
    Blocked,
    Bridge,
    Farm,
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
    Residential,
    Rocks,
    Snow,
    Storage
}
[ExecuteInEditMode]
public class TileSelector : MonoBehaviour
{
    public TileMaterials MaterialManager;

    public TileType type = TileType.Grass;
    TileType PreviousType;
    // Update is called once per frame
    void TileCreator()
    {
        if (type != PreviousType)
        {
            if (gameObject.GetComponent<TileNode>())
            {
                gameObject.GetComponent<TileNode>().Created = false;
                if (gameObject.GetComponent<TileNode>().CreatedObject)
                    DestroyImmediate(gameObject.GetComponent<TileNode>().TileGameObject);
            }
            foreach (var item in gameObject.GetComponents<TileNode>())
            {
                DestroyImmediate(item);
            }


            switch (type)
            {              
                case (TileType.Airport):
                    gameObject.AddComponent<Airport>();
                    break;
                case (TileType.Blocked):
                    gameObject.AddComponent<Blocked>();
                    break;
                case (TileType.Bridge):
                    gameObject.AddComponent<Bridge>();                   
                    break;
                case (TileType.Farm):
                    gameObject.AddComponent<Farm>();
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
                case (TileType.Residential):
                    gameObject.AddComponent<Residential>();
                    break; 
    
                case (TileType.Rocks):
                    gameObject.AddComponent<Rocks>();
                    break;
                case (TileType.Snow):
                    gameObject.AddComponent<Snow>();
                    break;    
                case (TileType.Storage):
                    gameObject.AddComponent<Storage>();
                    break;
         

            }

            PreviousType = type;

            gameObject.GetComponent<TileNode>().MaterialManager = MaterialManager;
            gameObject.GetComponent<TileNode>().enabled = true;

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