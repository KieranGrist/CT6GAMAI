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

    void Awake()
    {
        AIGen = this;
    }
    /// <summary>
    /// Places the AI units on randomly selected units. DOES NOT REQUIRE TO ADD AI AGENT
    /// </summary>
    public void PlaceAIUnits()
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
                }
            }
            while (Grids.Grid.GridList[num].Key);
            Grids.Grid.GridList[num] = new KeyValuePair<bool, GameObject>(true, Grids.Grid.GridList[num].Value);

        }
    }
    public void AIPersonalitySelector()
    {
      

    }

}
