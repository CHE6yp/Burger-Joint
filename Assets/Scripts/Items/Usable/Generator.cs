using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent(typeof(Usable))]
[RequireComponent(typeof(GroundItem))]
public class Generator : MonoBehaviour {
    Usable usable;

    public GameObject foodP;
    public GameObject wrapP;
    public GameObject binP;
    public GameObject meatP;
    public GameObject basketP;


    // Use this for initialization
    void Start () {


        usable = GetComponent<Usable>();

        usable.useDict.Add("Generate Food", GenerateFood);
        usable.useDict.Add("Generate Wrap", GenerateWrap);
        usable.useDict.Add("Generate BinWMeat", GenerateBinWithMeat);
        usable.useDict.Add("Generate Basket", GenerateBasket);

    }

    public void GenerateFood(Player player)
    {


        GameObject food = Instantiate(foodP);
        food.name = "Food";

        if (!player.itemPlace.hasItemPlaceds[0])
            food.GetComponent<Placable>().Place(player.itemPlace, 0);
        else
        if (player.itemPlace.items[0].GetComponent<Salver>() != null)
        {
            food.GetComponent<Placable>().Place(player.itemPlace.items[0].GetComponent<ItemPlace>(), 0);
        }

    }

    public void GenerateWrap(Player player)
    {
        GameObject wrap = Instantiate(wrapP);
        wrap.name = "Wrap";

        if (!player.itemPlace.hasItemPlaceds[0])
            wrap.GetComponent<Placable>().Place(player.itemPlace, 0);
        else
        if (player.itemPlace.items[0].GetComponent<Salver>() != null)
        {
            wrap.GetComponent<Placable>().Place(player.itemPlace.items[0].GetComponent<ItemPlace>(), 0);
        }

    }

    public void GenerateBinWithMeat(Player player)
    {

        if (!player.itemPlace.hasItemPlaceds[0])
        {
            GameObject salver = Instantiate(binP);
            salver.name = "Bin";
            GameObject meat = Instantiate(meatP);
            meat.name = "Meat";
            meat.GetComponent<Placable>().Place(salver.GetComponent<ItemPlace>(), 0);

            meat = Instantiate(meatP);
            meat.name = "Meat";
            meat.GetComponent<Placable>().Place(salver.GetComponent<ItemPlace>(), 1);

            meat = Instantiate(meatP);
            meat.name = "Meat";
            meat.GetComponent<Placable>().Place(salver.GetComponent<ItemPlace>(), 2);

            salver.GetComponent<Placable>().Take(player);





        }
    }

    public void GenerateBasket(Player player)
    {

        if (!player.itemPlace.hasItemPlaceds[0])
        {
            GameObject salver = Instantiate(basketP);
            salver.name = "Basket";
            

            salver.GetComponent<Placable>().Take(player);





        }
    }
}
