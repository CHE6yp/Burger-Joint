using UnityEngine;
using System.Collections;

public class CashierAI : AI {

    
    public static CashierAI onDisplay;

    public Register register;
    public GameObject display;

    public enum CashierState
    {
        Idle,
        WaitForOrder,
        AssembleOrder,
        WaitForDisplay,
        StandingInQueue,
        PutOrder,
        GiveOrder
    }
    public CashierState state = CashierState.WaitForOrder;

    public CashierState previousState;
    public GameObject previousDestination;


    void Start () {
        player = GetComponent<Player>();
        SetDestination2(register.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
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
            case CashierState.WaitForOrder:

                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        if (register.hasCustomer && register.hasOrder)
                        {

                            player.Use("Get Salver");
                            player.triggerObj.GetComponent<ItemPlace>().PlaceTake(player);

                            if (display.GetComponent<DisplayCounter>().cashier != null && display.GetComponent<DisplayCounter>().cashier != gameObject)
                            {
                                Debug.Log("CHECKED");
                                previousState = CashierState.AssembleOrder;
                                previousDestination = display;
                                state = CashierState.StandingInQueue;
                                GetInQueue(display.GetComponent<DisplayCounter>().cashier.GetComponent<Player>());

                            }
                            else
                            {
                                display.GetComponent<DisplayCounter>().cashier = gameObject;
                                SetDestination(display);
                                state = CashierState.AssembleOrder;
                            }
                        }
                    }
                    RotateTowards(destination.GetComponent<Transform>());
                }
                break;
            case CashierState.AssembleOrder:

                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        bool flag = false;
                        for (int i = 0; i < display.GetComponent<ItemPlace>().items.Length; i++)
                        {
                            if (display.GetComponent<ItemPlace>().items[i] != null && display.GetComponent<ItemPlace>().items[i].name == "Wrap"&&!flag)
                            {
                                display.GetComponent<ItemPlace>().items[i].GetComponent<Placable>().Take(player);

                                display.GetComponent<DisplayCounter>().cashier = null;
                                state = CashierState.PutOrder;
                                flag = true;


                                if (player.hasQueueAfter)
                                    player.queuePerson.GetComponent<CashierAI>().GetOutQueue();
                                //break;
                            }
                        }

                        if (!flag)
                        {
                            state = CashierState.WaitForDisplay;
                        }

                        /*
                        display.GetComponent<DisplayCounter>().GenerateFood(player);
                        state = CashierState.PutOrder;
                        */
                    }

                    RotateTowards(destination.GetComponent<Transform>());
                }
                
                break;
            case CashierState.WaitForDisplay:

                StartCoroutine("WaitCour");

                break;

            case CashierState.StandingInQueue:

                navigator.destination = destinationPlace.transform.position;
                if (ReachedDest())
                {
                    RotateTowards(destination.GetComponent<Transform>());
                }

                break;
            case CashierState.PutOrder:
                SetDestination2(register.gameObject);
                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        register.itemPlace.items[0].GetComponent<ItemPlace>().PlaceTake(player);//КОСТЫЫЫЫЫЫЛЬ
                        state = CashierState.GiveOrder;
                    }
                    RotateTowards(destination.GetComponent<Transform>());
                }
                break;
            case CashierState.GiveOrder:

                player.Use("Give Order");

                state = CashierState.WaitForOrder;
                break;

        }

    }


    IEnumerator WaitCour()
    {
        yield return new WaitForSeconds(1);
        if (state == CashierState.WaitForDisplay)
            state = CashierState.AssembleOrder;
    }



    void GetInQueue(Player anotherPerson)
    {

        if (!anotherPerson.hasQueueAfter)
        {
            anotherPerson.queuePerson = this.GetComponent<Player>();
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


}
