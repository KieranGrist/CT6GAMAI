using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct DesireDebug
{
    public float Status;
    public float DistanceToWayPoint;
    public float UnclappedDesire;

}
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
    public DesireDebug goDrive;
    public DesireDebug goOvertake;
    public DesireDebug goDefend;
    public DesireDebug goPit;
    public DesireDebug goShortcut;
    public DesireDebug goRandomItem;
    [Header("States")]
    public PQueue<AIAgent> AIAgentQueue = new PQueue<AIAgent>();

   // public Defend defendState;
    public Drive driveState;
    //public Overtake overtakeState;
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
            goDefend.Status = Artie.Artie_Defend;
            goDefend.DistanceToWayPoint = Helper.DistanceToItem(Artie.transform, Reference);
            goDefend.UnclappedDesire = K * (Artie.Artie_Defend / Helper.DistanceToItem(Artie.transform, Reference));
            DefendDesire = Mathf.Clamp(K * (Artie.Artie_Defend / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);
        }
        else
            DefendDesire = 0; //As this is the last car there is nothing to defend from


        Reference = null;
        Reference = Artie.TargetNode.transform;
        goDrive.Status = Artie.Artie_Drive;
        goDrive.DistanceToWayPoint = Helper.DistanceToItem(Artie.transform, Reference);
        goDrive.UnclappedDesire = K * (Artie.Artie_Drive / Helper.DistanceToItem(Artie.transform, Reference));
        DriveDesire = Mathf.Clamp(K * (Artie.Artie_Drive / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f); ;

        Reference = null;
        if (Artie.vehicle.RacePosition != 0)
        {
            Reference = LapManager.manager.CarPositions[Artie.vehicle.RacePosition - 1].transform;
            goOvertake.Status = Artie.Artie_OverTake;
            goOvertake.DistanceToWayPoint = Helper.DistanceToItem(Artie.transform, Reference);
            goOvertake.UnclappedDesire = K * (Artie.Artie_OverTake / Helper.DistanceToItem(Artie.transform, Reference));
            OvertakeDesire = Mathf.Clamp(K * (Artie.Artie_OverTake / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);

        }
        else
            OvertakeDesire = 0;
        Reference = Pitlane.pitlane.transform;
        goPit.Status = Artie.Artie_Pit;
        goPit.DistanceToWayPoint = Helper.DistanceToItem(Artie.transform, Reference);
        goPit.UnclappedDesire = K * (Artie.Artie_Pit / Helper.DistanceToItem(Artie.transform, Reference));
        PitDesire = Mathf.Clamp(K * (Artie.Artie_Pit / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);

        /* Not Implemented
        goRandomItem.Status = DrinkStatus();
        goRandomItem.DistanceToWayPoint = Helper.DistanceToItem(Artie.transform, Reference);
        goRandomItem.UnclappedDesire = K * (DrinkStatus() / Helper.DistanceToItem(Artie.transform, Reference));
        RandomItemDesire = Mathf.Clamp(K * (DrinkStatus() / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);

        goShortcut.Status = HomeStatus();
        goShortcut.DistanceToWayPoint = Helper.DistanceToItem(Artie.transform, Reference);
        goShortcut.UnclappedDesire = K * (HomeStatus() / Helper.DistanceToItem(Artie.transform, Reference));
        ShortcutDesire = Mathf.Clamp(K * (HomeStatus() / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);
        */
        RandomItemDesire = -1;
        ShortcutDesire = -1;
    }


    void CheckState()
    {
        AIAgentQueue.TaskQueue.Clear();

        CheckDesire();
        //AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(DefendDesire, defendState));
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(DriveDesire, driveState));
        //AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(OvertakeDesire, overtakeState));
        //AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(RandomItemDesire, randomItemState));
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(PitDesire, pitState));
        //AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(ShortcutDesire, shortcutState));

        AIAgentQueue.Sort();
        HighestDesire = AIAgentQueue.TaskQueue[AIAgentQueue.TaskQueue.Count - 1].Key;
        Artie.pState =  Transition<AIAgent>.Transist(Artie.pState, Artie.pState, AIAgentQueue.TaskQueue[AIAgentQueue.TaskQueue.Count - 1].Value, true);

    }

}
