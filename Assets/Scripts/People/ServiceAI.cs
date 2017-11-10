using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceAI : AI {

    

    public DisplayCounter displayC;
    public GameObject display;
    public GameObject generator;
    public GameObject assembly;

    public enum ServiceState
    {
        Idle,
        GetWrap,
        PutWrap,
        GetFood,
        WrapFood,
        PutFood,

    }

    public ServiceState state = ServiceState.Idle;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //if (displayC)

        StateMachine();
	}

    public override void StateMachine()
    {
        
        switch (state)
        {
            case ServiceState.Idle:
                if (displayC.itemPlace.placesTaken != 6)
                {
                    if (player.itemPlace.hasItemPlaceds[0] && player.itemPlace.items[0].tag == "Food")
                    {
                        SetDestination2(display);
                        state = ServiceState.PutFood;
                    }
                    else
                    {
                        SetDestination(generator);
                        state = ServiceState.GetWrap;
                    }
                }
                break;

            case ServiceState.GetWrap:
                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        player.Use("Generate Wrap");
                        SetDestination(assembly);
                        state = ServiceState.PutWrap;
                    }
                    RotateTowards(destination.GetComponent<Transform>());
                }
                break;
            case ServiceState.PutWrap:
                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        if (assembly.GetComponent<ItemPlace>().items[0] == null)
                        {
                            player.itemPlace.items[0].GetComponent<Placable>().Place(assembly.GetComponent<ItemPlace>(), 0);
                            //display.GetComponent<DisplayCounter>().cashier = null;
                            SetDestination(generator);
                            state = ServiceState.GetFood;

                            if (player.hasQueueAfter)
                                player.queuePerson.GetComponent<CashierAI>().GetOutQueue();
                            //break;
                        }
                    }
                    RotateTowards(destination.GetComponent<Transform>());
                }

                break;

            case ServiceState.GetFood:
                
                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        player.Use("Generate Food");
                        SetDestination(assembly);
                        state = ServiceState.WrapFood;

                    }
                    RotateTowards(destination.GetComponent<Transform>());
                }

                break;
            case ServiceState.WrapFood:
                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {
                        if (assembly.GetComponent<ItemPlace>().items[0] != null && assembly.GetComponent<ItemPlace>().items[0].name == "Wrap")
                        {
                            player.itemPlace.items[0].GetComponent<Placable>().Place(assembly.GetComponent<ItemPlace>().items[0].GetComponent<ItemPlace>(), 0);
                            //display.GetComponent<DisplayCounter>().cashier = null;
                            assembly.GetComponent<ItemPlace>().items[0].GetComponent<Placable>().Take(player);
                            SetDestination2(display);
                            state = ServiceState.PutFood;

                            if (player.hasQueueAfter)
                                player.queuePerson.GetComponent<CashierAI>().GetOutQueue();
                            //break;
                        }
                    }

                    RotateTowards(destination.GetComponent<Transform>());

                }
                break;
            case ServiceState.PutFood:
                if (ReachedDest())
                {
                    if (player.useBool && player.triggerObj == destination)
                    {

                        bool flag = false;
                        for (int i = 0; i < display.GetComponent<ItemPlace>().items.Length; i++)
                        {
                            if (display.GetComponent<ItemPlace>().items[i] == null && !flag)
                            {
                                player.itemPlace.items[0].GetComponent<Placable>().Place(display.GetComponent<ItemPlace>(), i);

                                //display.GetComponent<DisplayCounter>().cashier = null;
                                state = ServiceState.Idle;
                                flag = true;


                                if (player.hasQueueAfter)
                                    player.queuePerson.GetComponent<CashierAI>().GetOutQueue();
                                //break;
                            }

                        }
                    }


                    RotateTowards(destination.GetComponent<Transform>());

                }

                break;


        }


    }
}
