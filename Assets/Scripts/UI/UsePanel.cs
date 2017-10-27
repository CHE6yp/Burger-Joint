using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UsePanel : MonoBehaviour {

    public CanvasRenderer contextMenu;
    public Text objectName;
    public GameObject buttonPref;

    public List<GameObject> buttons = new List<GameObject>();

    // Use this for initialization
    void Start () {
	
	}

    //не нужен
    public void PanelDraw(Usable usable, Player player)
    {
        objectName.text = usable.gameObject.name;
        foreach (KeyValuePair<string,Usable.UsableFunc> func in usable.useDict)
        {
            GameObject buttonT = GameObject.Instantiate(buttonPref);
            buttons.Add(buttonT);
            buttonT.transform.SetParent(contextMenu.transform);
            Usable.UsableFunc funcT = func.Value;
            
            //string keyT = func.Key;
            
            //buttonT.GetComponent<Button>().onClick.AddListener(() => player.Use(keyT));
            buttonT.GetComponent<Button>().onClick.AddListener(() => funcT(player)); // ДА СУКА ДА
            
            //buttonT.GetComponent<Button>().onClick.AddListener(() =>  UIManager.uiManager.ContextRedraw(player.triggerObj, player));
            buttonT.transform.Find("Text").GetComponent<Text>().text = func.Key;
        }
    }

    public void PanelDraw(string objName, Dictionary<string,Usable.UsableFunc> useDict, Player player)
    {
        objectName.text = objName;
        foreach (KeyValuePair<string, Usable.UsableFunc> func in useDict)
        {
            GameObject buttonT = GameObject.Instantiate(buttonPref);
            buttons.Add(buttonT);
            buttonT.transform.SetParent(contextMenu.transform);
            Usable.UsableFunc funcT = func.Value;

            //string keyT = func.Key;

            //buttonT.GetComponent<Button>().onClick.AddListener(() => player.Use(keyT));
            buttonT.GetComponent<Button>().onClick.AddListener(() => funcT(player)); // ДА СУКА ДА

            //buttonT.GetComponent<Button>().onClick.AddListener(() =>  UIManager.uiManager.ContextRedraw(player.triggerObj, player));
            buttonT.transform.Find("Text").GetComponent<Text>().text = func.Key;
        }
    }
}
