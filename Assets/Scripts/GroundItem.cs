using UnityEngine;
using System.Collections.Generic;

public class GroundItem : MonoBehaviour {

    public List<Player> players = new List<Player>();

    public void ReChechUses()
    {
        foreach (Player player in players)
        {
            player.avUses.ClearUses();
            player.avUses.CollectUses();
            if (player.tag == "Player")
                UIManager.uiManager.ContextDraw(GetComponent<Usable>(), player);
        }
    }

}
