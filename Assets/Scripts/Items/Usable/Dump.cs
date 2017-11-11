using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent(typeof(Usable))]
public class Dump : MonoBehaviour {

    public Usable usable;
    // Use this for initialization
    void Start () {

        usable = GetComponent<Usable>();

        usable.useDict.Add("Throw Anything", ThrowAnything);

    }
	
    public void ThrowAnything(Player player)
    {
        Destroy(player.itemPlace.items[0]);
        
        player.itemPlace.hasItemPlaceds[0] = false;
    }
}
