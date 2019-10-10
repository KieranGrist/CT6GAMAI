using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ShopKeeperStateMachine
{
    public ShopKeeper shopKeeper;
    public Restock RestockState;
    public Shop ShopState;
    public Sleep SleepState;
    // Update is called once per frame
    public void Update()
    {
        CheckState();
    }
    public void CheckState()
    {
        //Restock State check
        Transition<ShopKeeper>.Transist(shopKeeper.pState, RestockState, ShopState, shopKeeper.s_TimeTillNewStock <= 0);
        
        //Shop State Checks
        Transition<ShopKeeper>.Transist(shopKeeper.pState, ShopState, SleepState, shopKeeper.s_Tiredness >= 15);
        Transition<ShopKeeper>.Transist(shopKeeper.pState, ShopState, RestockState, shopKeeper.s_TimeTillNewStock >= 10.0f);

        //Sleep State Checks
        Transition<ShopKeeper>.Transist(shopKeeper.pState, SleepState, ShopState, shopKeeper.s_Tiredness <= 0);
    }
}