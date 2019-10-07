using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }
}
