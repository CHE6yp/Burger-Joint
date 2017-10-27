using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent(typeof(Usable))]
public class Dump : MonoBehaviour {

    public Usable usable;
    // Use this for initialization
    void Start () {

        usable = GetComponent<Usable>();
        GetComponent<Usable>().use = ThrowAnything;

        usable.useDict.Add("Throw Anything", ThrowAnything);

        /*
        GetComponent<Usable>().use = ThrowAnything;

        GetComponent<Usable>().useArray = new Usable.UsableFunc[1];
        GetComponent<Usable>().useArray[0] = ThrowAnything;
        */
        
    }
	
    public void ThrowAnything(Player player)
    {
        GameObject.Destroy(player.itemPlace.items[0]);
        
        player.itemPlace.hasItemPlaceds[0] = false;
    }
}
