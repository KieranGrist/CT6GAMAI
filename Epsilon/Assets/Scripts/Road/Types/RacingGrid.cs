using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class to manage the grid of the track
/// </summary>
public class RacingGrid : MonoBehaviour
{
    public GameObject Grid1, Grid2, Grid3, Grid4, Grid5, Grid6, Grid7, Grid8; //Game objects representing the grid slots
    public static RacingGrid Grid; //Static refernce towards the grid 
    public List<KeyValuePair<bool, GameObject>> GridList = new List<KeyValuePair<bool, GameObject>>(); //List of key value pairs to track if the grid has been used yet or not
    // Start is called before the first frame update
    void Awake()
    {
        GridList.Clear(); //Clear the list 
           Grid = this; //Set the reference to be this
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid1)); //Create a new key value pair and add it to the list 
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid2)); //Create a new key value pair and add it to the list 
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid3)); //Create a new key value pair and add it to the list 
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid4)); //Create a new key value pair and add it to the list 
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid5)); //Create a new key value pair and add it to the list 
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid6)); //Create a new key value pair and add it to the list 
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid7)); //Create a new key value pair and add it to the list 
        GridList.Add(new KeyValuePair<bool, GameObject>(false,Grid8)); //Create a new key value pair and add it to the list 

    }

    // Update is called once per frame
    void Update()
    {
        Grid = this; //Set the reference to be this
    }
}
