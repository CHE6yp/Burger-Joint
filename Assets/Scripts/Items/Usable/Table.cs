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

    public GameObject person;//?

    void Awake()
    {
        itemPlace = GetComponent<ItemPlace>();
        personPlace = GetComponent<PersonPlace>();

        usable = GetComponent<Usable>();
        GetComponent<Usable>().use = SeatTable;

        usable.useDict.Add("Seat", SeatTable);
    }


    public void SeatTable(Player player)
    {

        if (person == null)
        {
            person = player.gameObject;
            person.transform.parent = personPlace.place.transform;

            person.transform.localPosition = new Vector3(0, 0, 0);

            //надо поменять управление чтобы реализовать нормально
            person.transform.localEulerAngles = new Vector3(0, 0, 0);

            player.seating = true;
            usable.Switch("Seat", "Stand", SeatTable);


        }
        else if (person == player.gameObject)
        {
            person.transform.parent = null;
            person = null;

            player.seating = false;

            usable.Switch("Stand", "Seat", SeatTable);
        }
    }
}
