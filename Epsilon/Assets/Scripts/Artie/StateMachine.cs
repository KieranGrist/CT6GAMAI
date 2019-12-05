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
    public float PitDesire;
    public float ShortcutDesire; //If one exists
    public float RandomItemDesire; //If exists
                                   //public float Artie_Aggresive;
    public float HighestDesire;
 
    [Header("States")]
    public PQueue<AIAgent> AIAgentQueue = new PQueue<AIAgent>();
    public Drive driveState;
    public Overtake overtakeState;
    public Pit pitState;
    float T;

    public void Update()
    {
        if (T > 2)
            CheckState();
        else
            T += Time.deltaTime;
    }
    public void CheckDesire()
    {
        DriveDesire = 0;
        OvertakeDesire = 0;
        PitDesire = 0;
        Transform Reference;
        Reference = null;
        Reference = Artie.targetNode.transform;
        if (Reference)
            DriveDesire = Mathf.Clamp(K * (Artie.artieDrive / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);
        Reference = null;
        var veh = Artie.steeringBehaviour.ProjectedCube.CheckForAI(Artie.vehicle);
        if(veh)
            Reference = veh.transform;
        if (Reference)
            OvertakeDesire = Mathf.Clamp(K * (Artie.artieOverTake / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);
        Reference = null;
        Reference = Pitlane.pitlane.transform;
        if (Reference)
            PitDesire = Mathf.Clamp(K * (Artie.artiePit / Helper.DistanceToItem(Artie.transform, Reference)), 0.0f, 1.0f);
    }


    void CheckState()
    {
        AIAgentQueue.TaskQueue.Clear();
        CheckDesire();
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(DriveDesire, driveState));
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(OvertakeDesire, overtakeState));
        AIAgentQueue.TaskQueue.Add(new KeyValuePair<float, State<AIAgent>>(PitDesire, pitState));
        AIAgentQueue.Sort();
        HighestDesire = AIAgentQueue.TaskQueue[AIAgentQueue.TaskQueue.Count - 1].Key;
        Artie.pState =  Transition<AIAgent>.Transist(Artie.pState, Artie.pState, AIAgentQueue.TaskQueue[AIAgentQueue.TaskQueue.Count - 1].Value, true);

    }

}
