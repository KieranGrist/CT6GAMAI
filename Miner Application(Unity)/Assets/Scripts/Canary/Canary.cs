using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Canary : MonoBehaviour
{
    public Transform c_TransformReference;
    public CanaryStateMachine c_StateMachine;
    public State<Canary> pState;
    public bool IsDead;
    //public members
    //These values can be monitored and editted by our "states"
    public int c_Singing;
    public int c_Flying;
    public int c_Dead;
    public Miner c_MinerReference;
    public float SleepTimer;
    // Start is called before the first frame update
    void Start()
    {
        c_StateMachine = new CanaryStateMachine();
        c_Singing = 0;
        c_Flying = 0;
        c_Dead = 0;
        c_StateMachine.DeathState = new Died();
        c_StateMachine.FlyState = new Fly();
        c_StateMachine.SingState = new Sing();
        c_StateMachine.PreviousState = c_StateMachine.SingState;
        pState = c_StateMachine.FlyState;
    }

    // Update is called once per frame
    void Update()
    {
        c_TransformReference = this.transform;
        SleepTimer += Time.deltaTime;
        if (SleepTimer >= 2)
        {
            SleepTimer = 0;
            c_StateMachine.canary = GetComponent<Canary>();
            c_StateMachine.Update();
            pState.Execute(GetComponent<Canary>());
        }
    }
}
