using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
[System.Serializable]
public class StateMachine
{
    public AIAgent Artie; //Reference to ai agent
     float K =1; //K value used in utility theory 
    [Header("Desire")]
     float DriveDesire; //Default Desire
     float OvertakeDesire; //Overtake Desire
     float PitDesire; //Pit Desire

 
    [Header("States")]
     PQueue<AIAgent> AIAgentQueue = new PQueue<AIAgent>(); //Priority queue for states
    public Drive driveState; //Drive State
    public Overtake overtakeState; //Overtake State
    public Pit pitState; //Pit stae 
    float T; //Timer

  public   void Update()
    {
        if (T > 2) // If AI has been alive for 2 seconds
            CheckState(); //Check the current state 
        else
            T += Time.deltaTime; //Increase timer by delta time
    }
    /// <summary>
    /// Check the state machines desires
    /// </summary>
     void CheckDesire()
    {
        DriveDesire = 0; //Set Desire to 0
        OvertakeDesire = 0; //Set Desire to 0
        PitDesire = 0; //Set Desire to 0
        Transform Reference; //Create a transform reference to be used in the distance desires
        Reference = null; //Set reference to be null
        Reference = Artie.targetNode.transform; //Set reference to be the target node transform
        if (Reference) //If reference exists 
            DriveDesire = Mathf.Clamp(K * (Artie.ArtieDrive / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f); //Get the drive state variable and divide it by the distance between artie and reference then times it by k and clamp it between 0 and 1
        Reference = null; //Set reference to be null
        var veh = Artie.steeringBehaviour.ProjectedCube.CheckForAI(Artie.vehicle); //Call check for AI function and set its return value to be veh
        if(veh) //If Vehicle exists
            Reference = veh.transform; //Set Reference to be vehicle transform
        if (Reference) //If Reference exists
            OvertakeDesire = Mathf.Clamp(K * (Artie.ArtieOverTake / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f); //Get the overtake state variable and divide it by the distance between artie and reference then times it by k and clamp it between 0 and 1
        Reference = null; //Set reference to null
        Reference = Pitlane.pitlane.transform; //Set reference to be pitlane transform
        if (Reference) //If reference exists
            PitDesire = Mathf.Clamp(K * (Artie.ArtiePit / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f); //Get the pit state variable and divide it by the distance between artie and reference then times it by k and clamp it between 0 and 1
    }
    /// <summary>
    /// Check the current state the machine needs to be in
    /// </summary>

    void CheckState()
    {
        AIAgentQueue.TaskQueue.Clear(); //Clear task queue
        CheckDesire(); //Call the check desire function
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(DriveDesire, driveState)); //Add the Keyvalue pair of Drive Desire and Drive state to the queue
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(OvertakeDesire, overtakeState)); //Add the Keyvalue pair of Overtake Desire and Overtake state to the queue
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(PitDesire, pitState)); //Add the Keyvalue pair of Pit Desire and Pit state to the queue
        AIAgentQueue.Sort(); //Sort the Queue
        Artie.pState = AIAgentQueue.TaskQueue[AIAgentQueue.TaskQueue.Count - 1].Value; //Switch the state to be the one with the highest value

    }

}
