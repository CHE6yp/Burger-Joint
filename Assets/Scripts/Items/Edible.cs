using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
[System.Serializable]
[RequireComponent(typeof(Usable))]
[RequireComponent(typeof(Placable))]
public class Edible : MonoBehaviour {


    public Usable usable;
    public Placable placable;
    //GameObject trash;

    public GameObject combinedP;
    public bool combined;
    public List<GameObject> ingridients = new List<GameObject>();

    public enum FoodType { Burger, Hotdog}
    public FoodType foodType = FoodType.Burger;

    // Use this for initialization
    void Awake () {

        placable = GetComponent<Placable>();
        usable = GetComponent<Usable>();

        usable.useDict.Add("Eat", Eat);
    }

    public void Eat(Player player)
    {
        if (!player.eating)
        {
            if (player.itemPlace.items[0] == null)
                GetComponent<Placable>().Take(player);

            player.eating = true;
            player.gameObject.GetComponent<Animator>().SetBool("eating", true);
        } else
        {
            player.eating = false;
            player.gameObject.GetComponent<Animator>().SetBool("eating", false);
        }
    }

    public void CraftCheck()
    {
        if (placable.itemPlaceOfParent.GetComponent<Edible>() != null)
        {
            if (placable.itemPlaceOfParent.GetComponent<Edible>().combined)
            {
                placable.itemPlaceOfParent.GetComponent<Edible>().ingridients.Add(gameObject);
                placable.itemPlaceOfParent.places[0] = GetComponent<ItemPlace>().places[0];
                placable.itemPlaceOfParent.items[0] = null;
            }
            else
            {
                GameObject food = Instantiate(combinedP, placable.itemPlaceOfParent.transform.position, Quaternion.identity);
                food.name = "Food";

                food.AddComponent<Edible>();
                food.GetComponent<Edible>().combined = true;

                placable.itemPlaceOfParent.transform.SetParent(food.transform);
                food.GetComponent<Placable>().Place(placable.itemPlaceOfParent.GetComponent<Placable>().itemPlaceOfParent, placable.itemPlaceOfParent.GetComponent<Placable>().placeIndex);
                food.GetComponent<Edible>().ingridients.Add(placable.itemPlaceOfParent.gameObject);
                

                gameObject.transform.SetParent(food.transform);
                food.GetComponent<Edible>().ingridients.Add(gameObject);
                



            }
        }

    }
}
