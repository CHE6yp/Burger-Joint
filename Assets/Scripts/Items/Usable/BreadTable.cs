using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent(typeof(ItemPlace))]
[RequireComponent(typeof(Usable))]
public class BreadTable : MonoBehaviour {

    Usable usable;
    ItemPlace itemPlace;
    public GameObject foodPref;

	// Use this for initialization
	void Start () {
        itemPlace = GetComponent<ItemPlace>();
        usable = GetComponent<Usable>();
        usable.useDict.Add("Sift", Sift);
        usable.useDict.Add("Transform", TempTrfmToFood);
    }
	

    public void Sift (Player player)
    {
        //if (itemPlace)
    }

    public void TempTrfmToFood(Player player)
    {
        if (itemPlace.items[0] != null)
        {
            Destroy(itemPlace.items[0]);
            itemPlace.items[0] = null;
            GameObject food = Instantiate(foodPref);
            food.name = "Food";
            food.GetComponent<Placable>().Insert(itemPlace, 0);
            food.GetComponent<Placable>().Place(itemPlace, 0);
        }

    }

}
