using UnityEngine;
[System.Serializable]

public class Miner : MonoBehaviour
{
    public Canary m_CanaryReference;
    public MinerStateMachine m_StateMachine;
    public int m_Day = 0;
    public int m_PickaxePower = 1;
    public int m_Gold = 0;
    public int m_BankedGold = 0;
    public int m_Tiredness = 0;
    public int m_Thirstiness = 0;
    public int m_Hunger = 0;
    public bool m_CanShop = false;
    public bool m_CheckedCanary = false;
    public bool FirstTime = true;
    public State<Miner> pState;
    public float SleepTimer =0;
    public Transform m_TransformReference;
    public ShopKeeper m_ShopKeeeperReference;
    public bool Moving = false;
    public float Distance;
    public float Speed;
    public Vector3 TargetLocation;
    public Vector3 Direction;
    public Vector3 Normalise;
    public Vector3 M;

    void Start()
    {
        m_StateMachine = new MinerStateMachine();
        m_StateMachine.BankingState = new BankingGold();
        m_StateMachine.CanaryState = new CheckCanary();
        m_StateMachine.EatState = new GoAndEat();
        m_StateMachine.DrinkState = new GoAndHaveADrink();
        m_StateMachine.HomeState = new GoHomeAndSleep();
        m_StateMachine.MiningState = new MiningForGold();
        m_StateMachine.ShoppingState = new GoToTheShop();
        pState = m_StateMachine.MiningState;
        m_PickaxePower = 1;
        Speed = 1.5f;
        m_StateMachine.miner = GetComponent<Miner>();
        m_StateMachine.Update();
        pState.Execute(GetComponent<Miner>());
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
            m_TransformReference = this.transform;
            SleepTimer += Time.deltaTime;
            if (SleepTimer >= 2)
            {
                SleepTimer = 0;
                m_StateMachine.miner = GetComponent<Miner>();
                m_StateMachine.Update();
                pState.Execute(GetComponent<Miner>());

            }
        }
    }
}
