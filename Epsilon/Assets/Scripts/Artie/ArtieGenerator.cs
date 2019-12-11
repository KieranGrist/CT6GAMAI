using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArtieGenerator : MonoBehaviour
{
    public GameObject Artie; //ARTIES game object to be generated
     bool Generated; //Has the cube been generated 
     LayerMask Mask; //Collision mask
    public static ArtieGenerator AIGen; //Static reference of this script
    // Start is called before the first frame update 
     List<GameObject> TempAI = new List<GameObject>(); //List of AI objets
    void Awake()
    {
        AIGen = this; //Set the static reference to the object calling it
    }
    /// <summary>
    /// Places the AI units on randomly selected units. DOES NOT REQUIRE TO ADD AI AGENT
    /// </summary>
    public void PlaceAIUnits()
    {
        TempAI = new List<GameObject>(); //Create a new list of AI
        for (int i = 0; i < 8; i++) //Create 8 AI
        {
            var num = 10; //Set number to be 10
            do
            {
                num = Random.Range(0, RacingGrid.Grid.GridList.Count); //Randomnise between 0 and the current grid list count
                if (!RacingGrid.Grid.GridList[num].Key) //If grid hasnt been used yet 
                {
                    GameObject go = Instantiate(Artie, transform.position, transform.rotation); //Create a new AI
                    go.transform.position = RacingGrid.Grid.GridList[num].Value.transform.position; //Set the position to be the grid slot chosen 
                    go.transform.localScale = new Vector3(1, 1, 1); //set scale
                    go.AddComponent<SteeringBehaviours>(); //add steering behaviors
                    TempAI.Add(go); //add ai to list
                }
            }
            while (RacingGrid.Grid.GridList[num].Key); //Loop until it finds a key that has not been used
            RacingGrid.Grid.GridList[num] = new KeyValuePair<bool, GameObject>(true, RacingGrid.Grid.GridList[num].Value);

        }
        
        var VehicleList = new List<KeyValuePair<bool, string>>();  //Create a new list of keyvalue pairs, this list is used to store the teams and if they have been assigned yet
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Ferrari")); //Create ferrari key pair
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Ferrari")); //Create ferrari key pair

        VehicleList.Add(new KeyValuePair<bool, string>(false, "Mercedes")); //Create Mercedes key pair
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Mercedes")); //Create Mercedes key pair

        VehicleList.Add(new KeyValuePair<bool, string>(false, "Ford")); //Create Ford key pair
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Ford")); //Create Ford key pair

        VehicleList.Add(new KeyValuePair<bool, string>(false, "Renault"));  //Create Renault key pair
        VehicleList.Add(new KeyValuePair<bool, string>(false, "Renault")); //Create Renault key pair

        foreach (var item in TempAI) //Loop through the AI in temp ai list
        {
            var num = 10; //Set number to be 10
            do
            {
                num = Random.Range(0, VehicleList.Count); //Randomnise a number between 0 and vehicle list count
                if (!VehicleList[num].Key) //If team hasnt been assigned
                {
                    switch (VehicleList[num].Value) //switch between the value of the key value pair
                    {
                        case "Ferrari": 
                            item.AddComponent<Ferrari>(); //Add ferrari vehicle
            
                            break;
                        case "Mercedes":
                            item.AddComponent<Mercedes>(); //Add Mercedes vehicle    
                            break;
                        case "Ford":
                            item.AddComponent<Ford>();    //Add Ford vehicle                       
                            break;
                        case "Renault":
                            item.AddComponent<Renault>();     //Add Renault vehicle                     
                            break;
                    }
                }
            }
            while (VehicleList[num].Key); //While the key has been used
            VehicleList[num] = new KeyValuePair<bool, string>(true, "Used"); //Set the bool to be true and the string to used that way it cant be used again

        }
    }

    /// <summary>
    /// Gives each AI their driver profile 
    /// </summary>
    public void AIPersonalitySelector()
    {

        foreach (var item in TempAI)        //Loop through all the AI 
        {
            bool Aggresive = Random.value >= 0.5; // Randomnise a value if its greater then or equal to 0.5 its true otherwise its false this is stored to a bool (Lambda funciton)
            if (Aggresive) //If Aggresive 
                item.AddComponent<Aggresive>(); //Add aggressive profile
            else
                item.AddComponent<Default>(); //Add default profile 
        }
    }
}
