using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
[System.Serializable]
public class StateMachine
{
    public AIAgent Artie;
    public float K =1;
    [Header("Desire")]
    public float DriveDesire; //Default State
    public float OvertakeDesire;
    public float DefendDesire;
    public float PitDesire;
    public float ShortcutDesire; //If one exists
    public float RandomItemDesire; //If exists
                                   //public float Artie_Aggresive;
    public float HighestDesire;
    public float SleepTimer;
    [Header("Debug")]

    [Header("States")]
    public PQueue<AIAgent> AIAgentQueue = new PQueue<AIAgent>();

   // public Defend defendState;
    public Drive driveState;
    public Overtake overtakeState;
    public Pit pitState;
    //public RandomItem randomItemState;
    //public Shortcut shortcutState;

    public void Update()
    {
        SleepTimer += Time.deltaTime;
        if (SleepTimer >= 2)
        {
            SleepTimer = 0;
            CheckState();
        }
    }
    public void CheckDesire()
    {
        Transform Reference;
        if (Artie.vehicle.RacePosition != 7)
        {
            Reference = LapManager.manager.CarPositions[Artie.vehicle.RacePosition + 1].transform;
            DefendDesire = Mathf.Clamp(K * (Artie.artieDefend / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);
        }
        else
            DefendDesire = 0; //As this is the last car there is nothing to defend from


        Reference = null;
        Reference = Artie.targetNode.transform;
        DriveDesire = Mathf.Clamp(K * (Artie.artieDrive / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f); ;

        Reference = null;
        if (Artie.vehicle.RacePosition != 0)
        {
            Reference = LapManager.manager.CarPositions[Artie.vehicle.RacePosition - 1].transform;
            OvertakeDesire = Mathf.Clamp(K * (Artie.artieOverTake / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);

        }
        else
            OvertakeDesire = 0;
        Reference = Pitlane.pitlane.transform;
        PitDesire = Mathf.Clamp(K * (Artie.artiePit / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);


    }


    void CheckState()
    {
        AIAgentQueue.TaskQueue.Clear();

        CheckDesire();
        //AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(DefendDesire, defendState));
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(DriveDesire, driveState));
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(OvertakeDesire, overtakeState));
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(PitDesire, pitState));
        AIAgentQueue.Sort();
        HighestDesire = AIAgentQueue.TaskQueue[AIAgentQueue.TaskQueue.Count - 1].Key;
        Artie.pState =  Transition<AIAgent>.Transist(Artie.pState, Artie.pState, AIAgentQueue.TaskQueue[AIAgentQueue.TaskQueue.Count - 1].Value, true);

    }

}
