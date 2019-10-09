using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ShopKeeperStateMachine
{
    public State<ShopKeeper> PreviousState;
    public ShopKeeper shopKeeper;
    public Restock RestockState;
    public Shop ShopState;
    public Sleep SleepState;

    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        CheckState();
    }
    public void ChangeState(State<ShopKeeper> newState)
    {
        PreviousState = null;
        PreviousState = shopKeeper.pState;

        shopKeeper.pState = null;
        shopKeeper.pState = newState;
    }

    public void CheckState()
    {
        if (shopKeeper.pState == RestockState)
        {
            RestockStateHandle();
        }
        if (shopKeeper.pState == ShopState)
        {
            ShopStateHandle();
        }
        if (shopKeeper.pState == SleepState)
        {
            SleepStateHandle();
        }

    }
    public void RestockStateHandle()
    {
        if (shopKeeper.s_TimeTillNewStock <= 0)
        { 
            Debug.Log("Ahh a brand new pickaxe");
            shopKeeper.s_TimeTillNewStock = 0;
            ChangeState(ShopState);
        }
    }
    public void ShopStateHandle()
    {
        if (shopKeeper.s_TimeTillNewStock >= 10)
        {
            Debug.Log("Darn, this pickaxe is out of date and rusted, better find a new one");
            ChangeState(RestockState);
        }
        if (shopKeeper.s_Tiredness >= 15)
        {
            Debug.Log("I am tired and need rest");
            ChangeState(SleepState);
        }
    }
    public void SleepStateHandle()
    {
        if (shopKeeper.s_Tiredness <= 0)
        {
            Debug.Log("I am ready for a new day !");
            shopKeeper.s_Tiredness = 0;
            ChangeState(ShopState);
        }
    }


}
