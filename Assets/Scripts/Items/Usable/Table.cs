using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent(typeof (ItemPlace))]
[RequireComponent(typeof(PersonPlace))]
[RequireComponent(typeof(Usable))]
public class Table : MonoBehaviour {

    public Usable usable;
    public ItemPlace itemPlace;
    public PersonPlace personPlace;

    //public IItem trash; //temperary

    public GameObject person;//?

    void Awake()
    {
        itemPlace = GetComponent<ItemPlace>();
        personPlace = GetComponent<PersonPlace>();

        usable = GetComponent<Usable>();
        GetComponent<Usable>().use = SeatTable;

        usable.useDict.Add("Seat", SeatTable);
        //usable.useDict.Add("Eat", EatTable);

    }


    public void SeatTable(Player player)
    {
        
        if (person == null)
        {
            //Debug.Log("SEATING!");
            person = player.gameObject;
            person.transform.parent = personPlace.place.transform;
            //person.transform.position = personPlace.place.transform.position;
            person.transform.localPosition = new Vector3(0, 0, 0);

            //надо поменять управление чтобы реализовать нормально
            person.transform.localEulerAngles = new Vector3(0, 0, 0);
            //person.transform.parent = null;
            player.seating = true;
            usable.Switch("Seat", "Stand", SeatTable);


        } else if (person == player.gameObject)
        {
            //Debug.Log("STANDING!");
            person.transform.parent = null;
            person = null;

            player.seating = false;

            usable.Switch("Stand", "Seat", SeatTable);
            //GetComponent<Usable>().use = UseTable;
        }
    }

    /*
    public void EatTable(Player player)
    {
        
        if (!(player.itemPlace.hasItemPlaceds[0]) && (itemPlace.hasItemPlaceds[0])&& (itemPlace.items[0].tag=="Salver"))
        {
            itemPlace.items[0].GetComponent<ItemPlace>().items[0].GetComponent<Placable>().Place(player.itemPlace, 0);
            player.itemPlace.items[0].GetComponent<Usable>().useDict["Eat"](player);
            
        }
        else
        if (player.itemPlace.hasItemPlaceds[0] && itemPlace.hasItemPlaceds[0])
        {
            player.itemPlace.items[0].GetComponent<Usable>().useDict["Eat"](player);
            player.itemPlace.items[0].GetComponent<Placable>().Place(itemPlace.items[0].GetComponent<ItemPlace>(), 0);
        }

    }
    */

}
