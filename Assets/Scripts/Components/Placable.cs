using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Usable))]
public class Placable : MonoBehaviour {

    Usable usable;

    public ItemPlace itemPlaceOfParent;
    public int placeIndex;

    void Awake()
    {
        usable = GetComponent<Usable>();

        usable.useDict.Add("Place",Place);
    }

    public void Place(Player player)
    {
        if (player.triggerObj != null)
            if (player.triggerObj.GetComponent<ItemPlace>() != null)
            {
                ItemPlace objItemPlace = player.triggerObj.GetComponent<ItemPlace>();
                if (objItemPlace.placeCount == 1 && !objItemPlace.hasItemPlaceds[0])
                    Place(objItemPlace, 0);
                else
                {
                    UIManager.uiManager.OptionalClear();
                    UIManager.uiManager.OptionalDraw(this, objItemPlace);
                }
            }
    }

    public void Place(ItemPlace placeTo, int index)
    {
        //КОСТЫЫЫЫЫЫЛЬ
        if (itemPlaceOfParent!=null&& itemPlaceOfParent.gameObject.tag=="Player")
            UIManager.uiManager.OptionalClear();

        if (!placeTo.hasItemPlaceds[index])
        {
            if (itemPlaceOfParent != null)
            {
                itemPlaceOfParent.items[placeIndex] = null;
                itemPlaceOfParent.hasItemPlaceds[placeIndex] = false;
            }

            placeTo.items[index] = gameObject;
            placeTo.hasItemPlaceds[index] = true;

            placeTo.items[index].transform.parent = placeTo.places[index].transform;
            placeTo.items[index].transform.position = placeTo.places[index].transform.position;
            placeTo.items[index].transform.localEulerAngles = new Vector3(0, 0, 0);

            itemPlaceOfParent = placeTo;
            itemPlaceOfParent.placesTaken++;

            placeIndex = index;
           
            if (placeTo.gameObject.GetComponent<Player>() == null )
                usable.Switch("Place", "Take", Take);
            
            UIManager.uiManager.ContextRedraw(PlayerController.player);

            //ПРОВЕРИТЬ ЕЩЕ РАЗ ЭТО ВСЕ, НЕДОДЕЛАННО!!!
            if (GetComponent<Edible>() != null)
            { 
                GetComponent<Edible>().CraftCheck();
            }
        }
    }

    public void Take(Player player)
    {
        if (!player.itemPlace.hasItemPlaceds[0])
        {
            if (itemPlaceOfParent != null)
            {
                itemPlaceOfParent.placesTaken--;
                itemPlaceOfParent.items[placeIndex] = null;
                itemPlaceOfParent.hasItemPlaceds[placeIndex] = false;
            }

            player.itemPlace.items[0] = gameObject;
            player.itemPlace.hasItemPlaceds[0] = true;

            player.itemPlace.items[0].transform.parent = player.itemPlace.places[0].transform;
            player.itemPlace.items[0].transform.position = player.itemPlace.places[0].transform.position;
            player.itemPlace.items[0].transform.localEulerAngles = new Vector3(0, 0, 0);
            
            itemPlaceOfParent = player.itemPlace;
            placeIndex = 0;

            usable.Switch("Take", "Place", Place);

            UIManager.uiManager.ContextRedraw(PlayerController.player);
        }


    }

    public void Insert(ItemPlace placeTo, int index)
    {
        placeTo.items[index] = gameObject;
        placeTo.hasItemPlaceds[index] = true;

        placeTo.items[index].transform.parent = placeTo.places[index].transform;
        placeTo.items[index].transform.position = placeTo.places[index].transform.position;
        placeTo.items[index].transform.localEulerAngles = new Vector3(0, 0, 0);

        itemPlaceOfParent = placeTo;

        UIManager.uiManager.ContextRedraw(PlayerController.player);
    }

}
