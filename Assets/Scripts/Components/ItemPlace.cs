using UnityEngine;
using System.Collections;

public class ItemPlace : MonoBehaviour {

   

    public int placeCount = 1;
    public int placesTaken = 0;
    
    public GameObject[] places;
    public GameObject[] items;
    public bool[] hasItemPlaceds;

    void Awake()
    {
        
        items = new GameObject[placeCount];
        places = new GameObject[placeCount];

        hasItemPlaceds = new bool[placeCount];
        
        int i = 0;
        foreach (Transform placeTr in transform)
        {
            if (placeTr.name == "ItemPlace" || placeTr.name == "FoodPlace")
            {
                places[i] = placeTr.gameObject;

                i++;
            }
        }
    }

    public void PlaceTake(Player player)
    {
        if (placeCount == 1)
            PlaceTake(player, 0);


    }

    public void PlaceTake(Player player, int i)
    {
        if (!(player.itemPlace.hasItemPlaceds[0]) && (hasItemPlaceds[i]))
        {
            items[i].GetComponent<Placable>().Place(player.itemPlace,0);
        }
        else if (player.itemPlace.hasItemPlaceds[0] && !hasItemPlaceds[i])
        {
            player.itemPlace.items[0].GetComponent<Placable>().Place(this, i);
            
        }
        else if (player.itemPlace.hasItemPlaceds[0] && hasItemPlaceds[i] && items[i].tag == "Salver")
        {
            items[i].GetComponent<Salver>().StackSalver(player);
        }
    }
}
