using UnityEngine;
using System.Collections.Generic;

[SelectionBase]
[RequireComponent(typeof(ItemPlace))]
[RequireComponent(typeof(PersonPlace))]
[RequireComponent(typeof(Usable))]
public class DisplayCounter : MonoBehaviour {

    public Usable usable;
    public ItemPlace itemPlace;
    public PersonPlace personPlace;

    public GameObject cashier;

    public GameObject[] foodArray = new GameObject[6];
    public GameObject[] foodPlace = new GameObject[6];
    //public HasItemPlace[] foodArray = new HasItemPlace[6];

    public GameObject foodP;
    public int foodCount = 0;
    public int foodMax = 6;
    public List<Edible> food = new List<Edible>();


    void Start () {  
        for (int i = 0; i < 7; i++)
        {
            //?? что я хотел? наверно наспаунить еды
        }

        usable = GetComponent<Usable>();

        /*
        GetComponent<Usable>().use = TakeFood;

        usable.useDict.Add("Take", TakeFood);
        usable.useDict.Add("Place", PlaceFood);
        */
        usable.useDict.Add("Generate", GenerateFood);
        usable.useDict.Add("Put", PutItem);
        usable.useDict.Add("Get", GetItem);

    }


    /*

    public void TakeFood(Player player)
    {
        for (int i = 0; i < 7; i++)
        {
            if (foodArray[i] != null)
            {
                if (!player.itemPlace.hasItemPlaced)
                {
                    ItemPlace pTo = player.GetComponent<ItemPlace>();

                    pTo.item = foodArray[i];
                    pTo.item.transform.parent = pTo.place.transform;
                    pTo.item.transform.position = pTo.place.transform.position;
                    pTo.item.transform.localEulerAngles = new Vector3(0, 0, 0);
                    foodArray[i] = null;
                    pTo.hasItemPlaced = true;

                    foodCount--;
                    break;
                }
                else if (player.itemPlace.item.GetComponent<Salver>() != null)
                {
                    ItemPlace pTo = player.GetComponent<ItemPlace>().item.GetComponent<ItemPlace>();

                    pTo.item = foodArray[i];
                    pTo.item.transform.parent = pTo.place.transform;
                    pTo.item.transform.position = pTo.place.transform.position;
                    pTo.item.transform.localEulerAngles = new Vector3(0, 0, 0);
                    foodArray[i] = null;
                    pTo.hasItemPlaced = true;

                    foodCount--;
                    break;
                }
                break;
            }
        }
    }

    public void PlaceFood(Player player)
    {
        if (foodCount != 6)
        {
            Debug.Log("?!");
            if (player.itemPlace.hasItemPlaced && player.itemPlace.item.GetComponent<Food>() != null)
            {
                
                //нужен утилити под этосамое
                for (int i = 0; i < 7; i++)
                {
                    if (foodArray[i] == null)
                    {
                        //HasItemPlace pTo = to.GetComponent<HasItemPlace>();
                        ItemPlace pFrom = player.GetComponent<ItemPlace>();
                        
                        foodArray[i] = pFrom.item;
                        foodArray[i].transform.parent = foodPlace[i].transform;
                        foodArray[i].transform.position = foodPlace[i].transform.position;
                        foodArray[i].transform.localEulerAngles = new Vector3(0, 0, 0);
                        pFrom.item = null;
                        
                        pFrom.hasItemPlaced = false;

                        foodCount++;
                        break;
                        
                    }
                }


            }
        }
    }
    */
    public void GenerateFood(Player player)
    {

        
        GameObject food = Instantiate(foodP);
        food.name = "Food";
        
        if (!player.itemPlace.hasItemPlaceds[0])
            food.GetComponent<Placable>().Place(player.itemPlace,0);
        else
        if (player.itemPlace.items[0].GetComponent<Salver>() != null)
        {
            food.GetComponent<Placable>().Place(player.itemPlace.items[0].GetComponent<ItemPlace>(),0);
        }


    }

    public void PutItem(Player player)
    {
        if (player.itemPlace.items[0].GetComponent<Edible>() != null && foodCount < foodMax)
        {
            food.Add(player.itemPlace.items[0].GetComponent<Edible>());
            player.itemPlace.items[0].GetComponent<Placable>().Place(itemPlace, foodCount);
            foodCount++;


        }
    }

    public void GetItem(Player player)
    {
        if (!player.itemPlace.hasItemPlaceds[0] && foodCount >0)
        {
            food.RemoveAt(0);
            Debug.Log(foodCount.ToString());
            Debug.Log(itemPlace.items[foodCount-1].name);
            itemPlace.items[foodCount-1].GetComponent<Placable>().Take(player);
            foodCount--;


        }
    }


}
