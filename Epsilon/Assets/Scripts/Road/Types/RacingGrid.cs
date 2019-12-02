using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingGrid : MonoBehaviour
{
    public GameObject Grid1, Grid2, Grid3, Grid4, Grid5, Grid6, Grid7, Grid8;
    public static RacingGrid Grid;
    public List<KeyValuePair<bool, GameObject>> GridList = new List<KeyValuePair<bool, GameObject>>();
    // Start is called before the first frame update
    void Awake()
    {
        GridList.Clear();
           Grid = this;
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid1));
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid2));
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid3));
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid4));
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid5));
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid6));
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid7));
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid8));

    }

    // Update is called once per frame
    void Update()
    {
        Grid = this;
    }
}
