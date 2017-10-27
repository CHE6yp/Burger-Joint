using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent(typeof(ItemPlace))]
[RequireComponent(typeof(Placable))]
[RequireComponent(typeof(Usable))]
public class Salver : MonoBehaviour {

    public Usable usable;
    public ItemPlace itemPlace;

    void Start()
    {
        usable = GetComponent<Usable>();
        itemPlace = GetComponent<ItemPlace>();
        //itemGO = gameObject;
    }


    public void StackSalver(Player player)
    {
        if (itemPlace.hasItemPlaceds[0])
        {
            if (itemPlace.items[0].tag == "Salver")
                itemPlace.items[0].GetComponent<Salver>().StackSalver(player);
        }
        else
            player.itemPlace.items[0].GetComponent<Placable>().Place(itemPlace, 0);
            //ItemPlace.TransferItem(player.itemPlace.places[0], GetComponent<Placable>().place);
    }

}
