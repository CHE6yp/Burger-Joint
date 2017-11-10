using UnityEngine;
using System.Collections;

public class BotAI : AI {

    public enum BotState
    {
        Idle,
        Outside,
        Deciding,
        GoToRegister,
        OrderingFood,
        WaitForOrder,
        GettingFood,
        StandingInQueue,
        GoToTable,
        Sitting,
        PlacingFoodOnTable,
        Eating,
        Standing,
        ThrowTrash,
        GoToExit
    }
    public BotState state = BotState.Outside;
    public BotState previousState = BotState.Idle;
    public GameObject previousDestination;

    public BotState checkingStateDebug = BotState.Idle;

    public int whereSpawned;
    public bool deciderFlag;

    public GameObject register;
    public GameObject table;
    public GameObject exit;
    public GameObject garbage;
    public Usable salverUsa;
    public Usable foodUsa;



    void Start()
    {

        register = JointManager.RandomRegister();
        table = JointManager.RandomTable();
        exit = JointManager.RandomExit();
        garbage = JointManager.RandomGarbage();

        JointManager.currentBotCount++;

        StateMachine();

    }

    void Update()
    {
        //НАДО ОТДЕЛЬНЫЙ КОНТРОЛЛЕР ДЛЯ АНИМАЦИИ
        if (navigator.velocity != new Vector3(0, 0, 0))
            GetComponent<Animator>().SetBool("move", true);
        else
            GetComponent<Animator>().SetBool("move", false);

        StateMachine();

    }

    public override void StateMachine()
    {
        switch (state)
        {
            //бот заспаунился и идет по улице навстречу терминации. по пути попадет в десайдер. когда закончил 
            //дела в ресторане, опять этот стейт
            case BotState.Outside:

                //navigator.destination = 

                if (whereSpawned == 1)
                {
                    navigator.destination = JointManager.jm.spawn2.transform.position;
                }
                else if (whereSpawned == 2) navigator.destination = JointManager.jm.spawn1.transform.position;


                if (whereSpawned == 3)
                {
                    navigator.destination = JointManager.jm.spawn4.transform.position;
                }
                else if (whereSpawned == 4) navigator.destination = JointManager.jm.spawn3.transform.position;

                break;

            //если на десайдере выпал крит, бот идет в ресторан
            case BotState.Deciding:

                SetDestination(register);
                state = BotState.GoToRegister;
                break;

            //в ресторане первым делом бот идет к кассе, чтобы сделать заказ
            case BotState.GoToRegister:
                
                if (register.GetComponent<Register>().hasCustomer && register.GetComponent<Register>().customer != player)
                {

                    previousState = state;
                    previousDestination = destination;
                    state = BotState.StandingInQueue;
                    GetInQueue(register.GetComponent<Register>().customer);
                    //state = BotState.StandingInQueue;
                }

                

                if (ReachedDest())
                {
                    //if (player.useBool)
                    register.GetComponent<Register>().customer = player;
                    register.GetComponent<Register>().hasCustomer = true;
                    state = BotState.OrderingFood;

                    
                }
                //RotateTowards(destination.GetComponent<Transform>());
                break;

            //если есть очередь, бот становится в нее
            case BotState.StandingInQueue:
                navigator.destination = destinationPlace.transform.position;
                if (ReachedDest())
                {
                    RotateTowards(destination.GetComponent<Transform>());
                }
                break;

            //дойдя до кассы, делает заказ
            case BotState.OrderingFood:
                if (player.useBool)
                {
                    register.GetComponent<Register>().ProcessOrder(player);
                    state = BotState.WaitForOrder;
                }
                RotateTowards(destination.GetComponent<Transform>());
                
                break;

            //ожидание заказа, берет его когда приносят
            case BotState.WaitForOrder:
                if (register.GetComponent<Register>().hasOrder&& register.GetComponent<Register>().orderReady
                    &&register.GetComponent<ItemPlace>().hasItemPlaceds[0])
                {

                    //Debug.Log("WAIT FFS!");
                    //player.Use("Order");
                    register.GetComponent<Register>().ProcessOrder(player);
                    //player.avUses.currentUses[player.triggerObj.GetComponent<Usable>()]["Order"](player);

                    if (player.hasQueueAfter)
                        player.queuePerson.GetComponent<BotAI>().GetOutQueue();


                    SetDestination(table);
                    state = BotState.GoToTable;
                    
                }
                RotateTowards(destination.GetComponent<Transform>());
                break;

            //взяв заказ, ищет себе столик и идет туда
            case BotState.GoToTable:


                if (table.GetComponent<ItemPlace>().hasItemPlaceds[0])
                {
                    table = JointManager.RandomTable();
                    SetDestination(table); //чтобы боты не садились за стол, на котором чтото есть
                }
                
                    

                if (ReachedDest())
                {
                    if (player.useBool)
                    {

                        state = BotState.Sitting;
                    }

                    RotateTowards(destination.GetComponent<Transform>());
                }
                break;

            //садится 
            case BotState.Sitting:
                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        table.GetComponent<Table>().SeatTable(player);
                        state = BotState.PlacingFoodOnTable;
                    }
                }
                break;

            //господи, ложит на стол еду
            case BotState.PlacingFoodOnTable:
                if (player.useBool)
                {
                    salverUsa.GetComponent<Placable>().Place(player);
                    state = BotState.Eating;
                }
                break;

            //ЕСТ
            case BotState.Eating:
                if (player.useBool && !player.eating)
                {
                    StartCoroutine("TestCour"); //Почти успех
                }
                RotateTowards(destination.GetComponent<Transform>());
                break;

            //как похавал, встает. решает, выкинуть мусор или оставить, потом съебывает 
            case BotState.Standing:
                if (player.useBool && player.triggerObj == destination)
                {
                    //player.Use("Stand");
                    table.GetComponent<Table>().SeatTable(player);
                    if (Random.Range(0, 2) > 0)
                    {
                        SetDestination(exit);
                        //state = BotState.GoToExit;
                        state = BotState.Outside;
                    }
                    else
                    {
                        salverUsa.GetComponent<Placable>().Take(player);
                        SetDestination(garbage);
                        state = BotState.ThrowTrash;
                    }
                    Rate();
                }
                break;

            //сознательные боты перед уходом выкидывают мусор
            case BotState.ThrowTrash:

                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        garbage.GetComponent<Garbage>().EmptySalverNew(player);
                        if (!garbage.GetComponent<ItemPlace>().hasItemPlaceds[0])
                           salverUsa.GetComponent<Placable>().Place(player);
                        else if (garbage.GetComponent<ItemPlace>().items[0].tag == "Salver")
                            garbage.GetComponent<ItemPlace>().items[0].GetComponent<Salver>().StackSalver(player);
                        //state = BotState.GoToExit;
                        state = BotState.Outside;
                    }

                    RotateTowards(destination.GetComponent<Transform>());
                }
                break;

        }

        CheckStateSwitch();
        //RotateTowards(destination.GetComponent<Transform>());
    }


    void CheckStateSwitch()
    {
        if (checkingStateDebug != state)
        {
            Debug.Log(gameObject.name + " state: " + state);
            checkingStateDebug = state;
        }
    }

    IEnumerator TestCour()
    {
        //player.Use(foodUsa,"Eat"); //Eat
        foodUsa.GetComponent<Edible>().Eat(player);
        //table.GetComponent<ItemPlace>().items[0].GetComponent<ItemPlace>().items[0].GetComponent<Food>().Eat(player);
        yield return new WaitForSeconds(5);
        //table.GetComponent<ItemPlace>().items[0].GetComponent<ItemPlace>().items[0].GetComponent<Food>().Eat(player);
        foodUsa.GetComponent<Edible>().Eat(player);
        foodUsa.GetComponent<Placable>().Place(salverUsa.GetComponent<ItemPlace>().items[0].GetComponent<ItemPlace>(), 0);
        
        state = BotState.Standing;
    }

    void GetInQueue(Player anotherPerson)
    {
        
        if (!anotherPerson.hasQueueAfter)
        {
            anotherPerson.queuePerson = this.player;
            destinationPlace = anotherPerson.queuePlace;

            anotherPerson.hasQueueAfter = true;
        }
        else
            GetInQueue(anotherPerson.queuePerson);
    }

    public void GetOutQueue()
    {
        SetDestination(previousDestination);
        state = previousState;
        
    }

    public void Rate()
    {
        int count = 10;
        foreach (GameObject table in JointManager.tables)
        {
            //if (table.GetComponent<ItemPlace>().hasItemPlaceds[0] && table.GetComponent<Table>().person == null)
            if (table.GetComponent<ItemPlace>().hasItemPlaceds[0])
                count--;
        }
        JointManager.cleanRating = count;
        //Debug.Log("Rated " + count.ToString());
    }

    void OnDestroy()
    {
        
        JointManager.currentBotCount--;
    }
}
