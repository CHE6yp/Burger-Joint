using UnityEngine;
using System.Collections.Generic;

public class AvailableUses : MonoBehaviour {


    Player player;
    //нужен диктионари с двумя ключами, предмет из которого идет функция, имя функции, и собственно валью функции
    [SerializeField]
    public Dictionary<Usable, Dictionary<string, Usable.UsableFunc>> currentUses
        = new Dictionary<Usable, Dictionary<string, Usable.UsableFunc>>();


    // Use this for initialization
    void Start() {
        player = GetComponent<Player>();
    }

    public void CollectUses()
    {
        if (player.itemPlace.hasItemPlaceds[0])
            currentUses.Add(player.itemPlace.items[0].GetComponent<Usable>(),
                player.itemPlace.items[0].GetComponent<Usable>().useDict);

        CollectCurrentUses(player.triggerObj.GetComponent<Usable>());

        //это было нужно когда игрок взаимодействовал через триггер с предметами. 
        //теперь будет через рейкаст, считать все предметы не нужно!
        //CollectUsesFromItems(player.triggerObj.GetComponent<Usable>());

        //currentUses.Add()
    }


    void CollectCurrentUses (Usable usable)
    {
        currentUses.Add(usable, usable.useDict);
    }

    //это было нужно когда игрок взаимодействовал через триггер с предметами. 
    //теперь будет через рейкаст, считать все предметы не нужно!
    void CollectUsesFromItems(Usable usable)
    {
        

        if (usable.gameObject.GetComponent<ItemPlace>() != null)
        {
            ItemPlace useItemPlace = usable.gameObject.GetComponent<ItemPlace>();
            //проверка на предметы 
            for (int i = 0; i < useItemPlace.placeCount; i++)
                if (useItemPlace.hasItemPlaceds[i]
                    && useItemPlace.items[i].GetComponent<Usable>() != null)
                {
                    Usable usable2 = usable.gameObject.GetComponent<ItemPlace>().items[i].GetComponent<Usable>();
                    CollectUsesFromItems(usable2);
                }
        }
    }


    public void ClearUses()
    {
        currentUses.Clear();
    }
}
