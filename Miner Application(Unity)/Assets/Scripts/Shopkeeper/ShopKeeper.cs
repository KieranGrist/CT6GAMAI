using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ShopKeeper : MonoBehaviour
{
    public Transform s_TransformReference;
    public ShopKeeperStateMachine s_StateMachine;
    public State<ShopKeeper> pState;
    public Miner s_MinerReference;
    public float s_TimeTillNewStock;
    public int s_Gold;
    public bool s_PickaxePurchased;
    public int s_Cost;
    public float s_Tiredness;
    public float SleepTimer;
    public bool Moving = false;
    public float Speed;
    public float Distance;
    public Vector3 TargetLocation;
    public Vector3 Direction;
    public Vector3 Normalise;
    public Vector3 M;
    // Start is called before the first frame update
    void Start()
    {
        s_StateMachine = new ShopKeeperStateMachine();
        s_TimeTillNewStock = 10;
        s_Cost = 2;
        s_Tiredness = 0;
        s_StateMachine.RestockState = new Restock();
        s_StateMachine.ShopState = new Shop();
        s_StateMachine.SleepState = new Sleep();
        pState = s_StateMachine.ShopState;
        s_StateMachine.shopKeeper = GetComponent<ShopKeeper>();
        s_StateMachine.Update();
        pState.Execute(GetComponent<ShopKeeper>());
        Speed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(TargetLocation, transform.position);
        if (transform.position != TargetLocation)
        {
            if (Distance <= 0.05)
            {
                transform.position = TargetLocation;
                Moving = false;
            }
            Moving = true;

        }
        else
        {
            Moving = false;
        }

        if (Moving == true)
        {
            // direction, normalise, direction * DeltaTime and speed, add that to current location
            Direction = TargetLocation - transform.position;
            Normalise = Direction.normalized;
            M = Normalise * Time.deltaTime * Speed;

            transform.position += M;
        }
        if (Moving == false)
        {
            s_TransformReference = this.transform;
            SleepTimer += Time.deltaTime;
            if (SleepTimer >= 0)
            {
                SleepTimer = 0;
                s_StateMachine.shopKeeper = GetComponent<ShopKeeper>();
                s_StateMachine.Update();
                pState.Execute(GetComponent<ShopKeeper>());
            }
        }
    }
}
