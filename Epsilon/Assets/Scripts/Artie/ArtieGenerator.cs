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
    public void PlaceAIUnits()
    {
        TempAI = new List<GameObject>();
        for (int i = 0; i < 8; i++)
        {
            var num = 10;
            do
            {
                num = Random.Range(0, RacingGrid.Grid.GridList.Count);
                if (!RacingGrid.Grid.GridList[num].Key)
                {
                    GameObject go = Instantiate(Artie, transform.position, transform.rotation);
                    go.transform.position = RacingGrid.Grid.GridList[num].Value.transform.position;
                    go.transform.localScale = new Vector3(1, 1, 1);
                    go.AddComponent<SteeringBehaviours>();
                    TempAI.Add(go);
                }
            }
            while (RacingGrid.Grid.GridList[num].Key);
            RacingGrid.Grid.GridList[num] = new KeyValuePair<bool, GameObject>(true, RacingGrid.Grid.GridList[num].Value);

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

        foreach (var item in TempAI)
        {
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
            
                            break;
                        case "Mercedes":
                            item.AddComponent<Mercedes>();                   
                            break;
                        case "Ford":
                            item.AddComponent<Ford>();                       
                            break;
                        case "Renault":
                            item.AddComponent<Renault>();               
                            break;
                    }
                }
            }
            while (VehicleList[num].Key);
            VehicleList[num] = new KeyValuePair<bool, string>(true, "Used");

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
    }
    public void AIPersonalitySelector()
    {      

        foreach(var item in TempAI)
        {
          bool Aggresive = Random.value >= 0.5;
            if (Aggresive)
                item.AddComponent<Aggresive>();
            else
                    item.AddComponent<Default>();
        }
    }
}
