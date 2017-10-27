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
        
        /*
        if (GetComponent<Player>() == null)
            GetComponent<Usable>().useDict.Add("Place/Take", PlaceTake);
            */
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
        else
        if (player.itemPlace.hasItemPlaceds[0] && !hasItemPlaceds[i])
        {
            player.itemPlace.items[0].GetComponent<Placable>().Place(this, i);
            
        }
        else if (player.itemPlace.hasItemPlaceds[0] && hasItemPlaceds[i] && items[i].tag == "Salver")
        {
            items[i].GetComponent<Salver>().StackSalver(player);
        }
        
    }

    /*
    static public void TransferItem(Place from, Place to)
    {

        to.item = from.item;
        to.item.transform.parent = to.place.transform;
        to.item.transform.position = to.place.transform.position;
        to.item.transform.localEulerAngles = new Vector3(0, 0, 0);
        from.item = null;
        to.hasItemPlaced = true;
        from.hasItemPlaced = false;


        //затратно?
        UIManager.uiManager.ContextRedraw(PlayerController.player.triggerObj, PlayerController.player);

    }
    */
}
