using UnityEngine;
using System.Collections;

public class LobbyAI : AI {

    public enum BotState
    {
        Idle,
        TakeSalver,
        ThrowTrash,
        TakeOutTrash,
        ThrowToDump
    }
    public BotState state = BotState.Idle;
    public BotState previousState = BotState.Idle;
    public BotState checkingStateDebug = BotState.Idle;


    public GameObject register;
    public GameObject table;
    public GameObject exit;
    public GameObject garbage;
    public GameObject dump;
    public Usable salverUsa;
    public Usable foodUsa;

    public bool hasQueueAfter;
    public BotAI queueCustomer;
    public GameObject queuePlace;


    void Start()
    {
        
        table = JointManager.RandomTable();
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


        //navigator.destination = destinationPlace.transform.position;
        //RotateTowards(destination.GetComponent<Transform>());
        //navigator.destination = destinationPlace.transform.position;


    }

    public override void StateMachine()
    {
        switch (state)
        {
            case BotState.Idle:

                foreach (GameObject tableTemp in JointManager.tables)
                {
                    if (tableTemp.GetComponent<ItemPlace>().hasItemPlaceds[0] && tableTemp.GetComponent<Table>().person == null)
                    {
                        //Debug.Log("GARBAGE TAKOUT!");
                        table = tableTemp;
                        state = BotState.TakeSalver;

                    }
                }

                //поменять эту проверку на ивент
                foreach (GameObject garbageTemp in JointManager.garbages)
                {
                    if (garbageTemp.GetComponent<Garbage>().max == garbageTemp.GetComponent<Garbage>().garbage)
                    {
                        Debug.Log("GARBAGE TAKOUT!");
                        garbage = garbageTemp;
                        state = BotState.TakeOutTrash;
                        
                    }
                }
                
                //RotateTowards(destination.GetComponent<Transform>());
                break;
            //фурычит плохо, ДОДЕЛАТЬ!
            case BotState.TakeSalver:
                SetDestination(table);

                if (!(table.GetComponent<ItemPlace>().hasItemPlaceds[0] && table.GetComponent<Table>().person == null))
                {
                    state = BotState.Idle;
                }

                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        table.GetComponent<ItemPlace>().items[0].GetComponent<Placable>().Take(player);

                        garbage = JointManager.RandomGarbage();
                        state = BotState.ThrowTrash;
                    }
                    RotateTowards(destination.GetComponent<Transform>());
                }

                break;
            case BotState.ThrowTrash:

                SetDestination(garbage);
                

                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        player.Use("Throw Garbage");
                        if (!garbage.GetComponent<ItemPlace>().hasItemPlaceds[0])
                            //player.Use(salverUsa, "Place");
                            player.itemPlace.items[0].GetComponent<Placable>().Place(player);
                        else if (garbage.GetComponent<ItemPlace>().items[0].tag == "Salver")
                            garbage.GetComponent<ItemPlace>().items[0].GetComponent<Salver>().StackSalver(player);
                        state = BotState.Idle;
                    }

                    RotateTowards(destination.GetComponent<Transform>());
                }
                break;
            case BotState.TakeOutTrash:

                SetDestination(garbage);
                if (garbage.GetComponent<Garbage>().max != garbage.GetComponent<Garbage>().garbage)
                {
                    state = BotState.Idle;
                }

                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        player.Use("Take Out Trash");
                        
                        state = BotState.ThrowToDump;
                    }

                    RotateTowards(destination.GetComponent<Transform>());
                }

                break;
            case BotState.ThrowToDump:

                SetDestination(dump);

                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        player.Use("Throw Anything");

                        state = BotState.Idle;
                    }

                    RotateTowards(destination.GetComponent<Transform>());
                }

                break;
        }

        //RotateTowards(destination.GetComponent<Transform>());
        CheckStateSwitch();
    }

    void CheckStateSwitch()
    {
        if (checkingStateDebug != state)
        {
            Debug.Log(gameObject.name + " state: " + state);
            checkingStateDebug = state;
        }
    }

}
