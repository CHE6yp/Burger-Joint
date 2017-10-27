using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionalPanel : MonoBehaviour {

    public delegate void OptionalFunc(ItemPlace itemPlace, int index);
    //public OptionalFunc use;

    public CanvasRenderer contextMenu;
    public Text objectName;
    public GameObject buttonPref;

    public List<GameObject> buttons = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    public void PanelDraw(Placable placable, ItemPlace itemPlace)
    {
        objectName.text = itemPlace.gameObject.name;
        for (int i = 0;i<itemPlace.placeCount;i++)
        {
            if (!itemPlace.hasItemPlaceds[i])
            {
                GameObject buttonT = GameObject.Instantiate(buttonPref);
                buttons.Add(buttonT);
                buttonT.transform.SetParent(contextMenu.transform);
                OptionalFunc funcT = placable.Place;

                int index = i;

                //buttonT.GetComponent<Button>().onClick.AddListener(() => player.Use(keyT));
                buttonT.GetComponent<Button>().onClick.AddListener(() => funcT(itemPlace, index)); // ДА СУКА ДА

                //buttonT.GetComponent<Button>().onClick.AddListener(() =>  UIManager.uiManager.ContextRedraw(player.triggerObj, player));
                buttonT.transform.Find("Text").GetComponent<Text>().text = (index + 1).ToString();
            }
        }
    }

}
