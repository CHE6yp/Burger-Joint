using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent(typeof(ItemPlace))]
[RequireComponent(typeof(PersonPlace))]
[RequireComponent(typeof(Usable))]
public class Garbage : MonoBehaviour {

    public Usable usable;
    public ItemPlace itemPlace;
    public PersonPlace personPlace;

    public TextMesh counter;
    public int food = 0;
    public int garbage = 0;
    public int max = 6;
    public GameObject garbageBag;


    void Start()
    {
        itemPlace = GetComponent<ItemPlace>();
        personPlace = GetComponent<PersonPlace>();

        usable = GetComponent<Usable>();

        usable.useDict.Add("Throw Garbage", EmptySalverNew);
        usable.useDict.Add("Take Out Trash", TakeOutTrash);
    }

    
    public void EmptySalverNew(Player player)
    {
        if (garbage < max)
        {
            if (player.itemPlace.hasItemPlaceds[0])
            {

                if (player.itemPlace.items[0].GetComponent<ItemPlace>() != null)
                {
                    ItemPlace itemT = player.itemPlace.items[0].GetComponent<ItemPlace>();

                    if (itemT.items[0] != null)
                    {
                        garbage++;

                        GameObject.Destroy(itemT.items[0]);
                        itemT.items[0] = null;
                        itemT.hasItemPlaceds[0] = false;
                        counter.text = garbage.ToString() + "/" + max.ToString();
                    }
                }

            }
        }
        else
        {
            Debug.Log("Garbage is full!");
            if (player.itemPlace.hasItemPlaceds[0])
            {

                if (player.itemPlace.items[0].GetComponent<ItemPlace>() != null)
                {
                    
                    ItemPlace itemT = player.itemPlace.items[0].GetComponent<ItemPlace>();
                    
                    if (itemT.items[0] != null)
                    {
                        GameObject item = itemT.items[0];
                        
                        itemT.items[0] = null;
                        itemT.hasItemPlaceds[0] = false;

                        item.transform.parent = null;

                        float rX = Random.Range(-2.5f, 2.5f);
                        float rZ = Random.Range(-2.5f, -1);
                        item.transform.position = transform.position + new Vector3(rX, -2, rZ); 

                    }
                }

            }
        }
        
    }
    


    public void TakeOutTrash(Player player)
    {
        if (!player.itemPlace.hasItemPlaceds[0])
        {
            GameObject garbageT = Instantiate(garbageBag);
            garbageT.GetComponent<GarbageBag>().trash = garbage;
            garbageT.GetComponent<Placable>().Place(player.itemPlace, 0);
            //Utility.PlaceItem(garbageT, player.gameObject);
            garbage = 0;
            counter.text = garbage.ToString() + "/" + max.ToString();
        }
    }


}
