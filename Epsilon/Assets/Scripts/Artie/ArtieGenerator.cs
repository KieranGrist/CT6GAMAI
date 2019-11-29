using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArtieGenerator : MonoBehaviour
{
    public GameObject Artie;
    public bool Generated;
    public LayerMask Mask;
    public static ArtieGenerator AIGen;
    // Start is called before the first frame update
     List<GameObject> TempAI = new List<GameObject>();
    void Awake()
    {
        AIGen = this;
    }
    /// <summary>
    /// Places the AI units on randomly selected units. DOES NOT REQUIRE TO ADD AI AGENT
    /// </summary>
    public string PlaceAIUnits()
    {
        for (int i = 0; i < 7; i++)
        {
            var num = 10;
            do
            {
                num = Random.Range(0, Grids.Grid.GridList.Count);
                if (!Grids.Grid.GridList[num].Key)
                {
                    GameObject go = Instantiate(Artie, transform.position, transform.rotation);
                    go.transform.position = Grids.Grid.GridList[num].Value.transform.position;             
                    go.transform.localScale = new Vector3(1, 1, 1);
                    TempAI.Add(go);
                }
            }
            while (Grids.Grid.GridList[num].Key);

        
        


            Grids.Grid.GridList[num] = new KeyValuePair<bool, GameObject>(true, Grids.Grid.GridList[num].Value);
        
        }
        var TBD = new GameObject();
        var VehicleList = new List<KeyValuePair<bool, string>>();
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Ferrari"));
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Ferrari"));

        VehicleList.Add(new KeyValuePair<bool, string>(false, "Mercedes"));
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Mercedes"));

        VehicleList.Add(new KeyValuePair<bool, string>(false, "Ford"));
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Ford"));

        VehicleList.Add(new KeyValuePair<bool, string>(false, "Renault"));
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Renault"));

  foreach(var item in TempAI) {
            var num = 10;
            do
            {
                num = Random.Range(0, VehicleList.Count);
                if (!VehicleList[num].Key)
                {
                    switch (VehicleList[num].Value)
                    {
                        case "Ferrari":
                            item.AddComponent<Ferrari>();
                            VehicleList[num] = new KeyValuePair<bool, string>(true, "Used");
                            break;
                        case "Mercedes":
                            item.AddComponent<Mercedes>();
                            VehicleList[num] = new KeyValuePair<bool, string>(true, "Used");
                            break;
                        case "Ford":
                            item.AddComponent<Ford>();
                            VehicleList[num] = new KeyValuePair<bool, string>(true, "Used");
                            break;
                        case "Renault":
                            item.AddComponent<Renault>();
                            VehicleList[num] = new KeyValuePair<bool, string>(true, "Used");
                            break;
                    }
                }
            }
            while (Grids.Grid.GridList[num].Key);





            Grids.Grid.GridList[num] = new KeyValuePair<bool, GameObject>(true, Grids.Grid.GridList[num].Value);

        }

        var T = 0; var I = 0;
        foreach (var item in VehicleList)
        {
            if (!item.Key)
            {
                T = I;
                break;
            }
            I++;
        }
        Destroy(TBD);
        return VehicleList[T].Value;
    }
    public void AIPersonalitySelector()
    {
      
        foreach(var item in TempAI)
        {
            item.AddComponent<Default>();

        }
    }

}
