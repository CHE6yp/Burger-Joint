using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerTrigger : MonoBehaviour {
    public Player player;
    public Collider currentCollider;
    public bool hasObject;
    public UIManager ui;
    

	void OnTriggerEnter(Collider other)
    {
        if (!hasObject && other.gameObject.GetComponent<Usable>() != null)
        {
            
            Usable usable = other.gameObject.GetComponent<Usable>();
            player.Triggered(other);
           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Usable>() != null)
        {
            //player.avUses.currentUses.Clear();
            player.UnTriggered(other);
            
            if (player.gameObject.tag == "Player")
            {
                //GameObject.Find("UsableText").GetComponent<Text>().text = "";
                Debug.Log("ContextClear");
                ui.ContextClear();
            }
            hasObject = false;
        }
    }
}
