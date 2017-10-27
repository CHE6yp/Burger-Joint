using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {

    /*
    /// <summary>
    /// Transfer an item from one place to another.
    /// </summary>
    /// <param name="pFrom"></param>
    /// <param name="pTo"></param>
	static public void TransferItem(GameObject from, GameObject to)
    {
        ItemPlace pTo = to.GetComponent<ItemPlace>();
        ItemPlace pFrom = from.GetComponent<ItemPlace>();

        pTo.item = pFrom.item;
        pTo.item.transform.parent = pTo.place.transform;
        pTo.item.transform.position = pTo.place.transform.position;
        pTo.item.transform.localEulerAngles = new Vector3(0, 0, 0);
        pFrom.item = null;
        pTo.hasItemPlaced = true;
        pFrom.hasItemPlaced = false;


        //затратно?
        

    }

    /// <summary>
    /// Place an item into IHasItemPlace. Doesn't reqire starting location.
    /// </summary>
    /// <param name="pFrom"></param>
    /// <param name="pTo"></param>
    static public void PlaceItem(GameObject item, GameObject pTo)
    {
        if (item.GetComponent<Placable>() != null)
        {
            if (item.GetComponent<Placable>().itemPlaceOfParent != null)
            {
                item.GetComponent<Placable>().itemPlaceOfParent.GetComponent<ItemPlace>().item = null;
                item.GetComponent<Placable>().itemPlaceOfParent.GetComponent<ItemPlace>().hasItemPlaced = false;
            }
            ItemPlace to = pTo.GetComponent<ItemPlace>();

            to.item = item;
            Debug.Log(to.name);
            to.item.transform.parent = to.places[0].place.transform;
            to.item.transform.position = to.places[0].place.transform.position;
            to.item.transform.localEulerAngles = new Vector3(0, 0, 0);
            to.hasItemPlaced = true;

            item.GetComponent<Placable>().itemPlaceOfParent = to;
        }
        

    }
    */

}
