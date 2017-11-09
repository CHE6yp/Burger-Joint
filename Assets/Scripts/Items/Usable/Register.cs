using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent(typeof(ItemPlace))]
[RequireComponent(typeof(PersonPlace))]
[RequireComponent(typeof(Usable))]
public class Register : MonoBehaviour
{

    public Usable usable;
    public ItemPlace itemPlace;
    public PersonPlace personPlace;
    public AudioSource audioSource;

   
    public bool hasCustomer;
    public Player customer;
    public bool hasOrder = false;
    public bool orderReady = false;

    public int cash = 0;

    public GameObject salverP;
    public GameObject foodP;

    // Use this for initialization
    void Start()
    {
        itemPlace = GetComponent<ItemPlace>();
        personPlace = GetComponent<PersonPlace>();
        audioSource = GetComponent<AudioSource>();

        usable = GetComponent<Usable>();
        usable.use = ProcessOrder;

        usable.useDict.Add("Generate w/ Food", GenerateSalverWithFood);
        usable.useDict.Add("Order", ProcessOrder);
        usable.useDict.Add("Give Order", GiveOrder);
        usable.useDict.Add("Get Salver", GenerateSalver);

    }


    public void GenerateSalver(Player player)
    {

        if (!player.itemPlace.hasItemPlaceds[0])
        {
            GameObject salver = Instantiate(salverP);
            salver.name = "Salver";
            salver.GetComponent<Placable>().Place(player.itemPlace,0);
        }
    }

    public void GenerateSalverWithFood(Player player)
    {

        if (!player.itemPlace.hasItemPlaceds[0])
        {
            GameObject salver = Instantiate(salverP);
            salver.name = "Salver";
            GameObject food = Instantiate(foodP);
            food.name = "Food";
            food.GetComponent<Placable>().Place(salver.GetComponent<ItemPlace>(), 0);

            salver.GetComponent<Placable>().Insert(player.itemPlace,0);
            
            

            

        }
    }

    void GenerateFood(Player player)
    {
        GameObject food = Instantiate(foodP);
        food.name = "Food";
        food.GetComponent<Placable>().Place(player.itemPlace.items[0].GetComponent<ItemPlace>(), 0);
    }

    public void ProcessOrder(Player player)
    {
        hasOrder = true;
        //Debug.Log("ProcessOrder(" + player.gameObject.name +")");
        if (!hasOrder)//костыль
        {
            //Debug.Log("ORDER FFS!");
            customer = player;
            hasCustomer = true;
            hasOrder = true;
            
            //Debug.Log("CUST = "+customer.gameObject.name);
        }
        else if (itemPlace.hasItemPlaceds[0]&&orderReady)
        {
            //Debug.Log("TAKE FOOD FFS!");
            PayForFood(customer);
            //UseTable(customer);
            
            customer.GetComponent<BotAI>().salverUsa = itemPlace.items[0].GetComponent<Usable>();
            customer.GetComponent<BotAI>().foodUsa = 
                itemPlace.items[0].GetComponent<ItemPlace>().items[0].GetComponent<ItemPlace>().items[0].GetComponent<Usable>();
            Debug.Log("TriggerObj -"+ customer.triggerObj);
            itemPlace.items[0].GetComponent<Placable>().Take(customer);
            Debug.Log(itemPlace.items[0]);
            //GetComponent<ItemPlace>().items[0]. (customer);

            customer = null;
            //Debug.Log("CUST = NULL, "+hasCustomer);
            hasCustomer = false;
            //Debug.Log("hasCustomer " + hasCustomer);
            hasOrder = false;
            orderReady = false;
        }
    }

    public void GiveOrder(Player player)
    {
        orderReady = true;
    }

    public void PayForFood(Player player)
    {
        audioSource.Play();
        player.cash -= 50;
        cash += 50;
    }
}
        

