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
    public int s_TimeTillNewStock;
    public int s_Gold;
    public bool s_PickaxePurchased;
    public int s_Cost;
    public int s_Tiredness;
    public float SleepTimer; 
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
            
    }

    // Update is called once per frame
    void Update()
    {
        s_TransformReference = this.transform;
        SleepTimer += Time.deltaTime;
        if (SleepTimer >= 2)
        {
            SleepTimer = 0;
            s_StateMachine.shopKeeper = GetComponent<ShopKeeper>();
            s_StateMachine.Update();
            pState.Execute(GetComponent<ShopKeeper>());
        }
    }
}
