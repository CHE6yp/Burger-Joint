using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent(typeof(ItemPlace))]
public class Player : MonoBehaviour {

    public bool useBool;
    public GameObject triggerObj;
    public GameObject useShow;

    public AvailableUses avUses;

    public ItemPlace itemPlace;


    public bool hasQueueAfter;
    public Player queuePerson;
    public GameObject queuePlace;

    public bool seating = false;
    public bool eating = false;
    public int cash = 300;


    void Start()
    {

        itemPlace = GetComponent<ItemPlace>();
    }

    void Update()
    {
        if (itemPlace.hasItemPlaceds[0])
            GetComponent<Animator>().SetBool("holding", true);
        else
            GetComponent<Animator>().SetBool("holding", false);
    }

    public void Use(string funcName)
    {
        try { 
            triggerObj.GetComponent<Usable>().useDict[funcName](this);
        
        }
        catch
        {
            Debug.LogWarning("useDict[" + funcName + "] problem. " + gameObject.name );
        }
    }

    public void Triggered(Collider other)
    {
        if (other.gameObject.GetComponent<Usable>() != null)
        {
            triggerObj = other.gameObject;
            useShow.SetActive(true);
            useBool = true;
            if (other.gameObject.GetComponent<GroundItem>() != null)
                other.gameObject.GetComponent<GroundItem>().players.Add(this);
        }
    }

    public void UnTriggered(Collider other)
    {
        if (other.gameObject.GetComponent<GroundItem>() != null)
            other.GetComponent<GroundItem>().players.Remove(this);
        triggerObj = null;
        useShow.SetActive(false);
        useBool = false;
    }
}
