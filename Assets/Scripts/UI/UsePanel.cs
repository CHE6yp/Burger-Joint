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
    //ЕЩЕ КАК НУЖЕН КАК ОКАЗАЛОСЬ
    public void PanelDraw(Usable usable, Player player)
    {
        objectName.text = usable.gameObject.name;
        foreach (KeyValuePair<string,Usable.UsableFunc> func in usable.useDict)
        {
            GameObject buttonT = Instantiate(buttonPref);
            buttons.Add(buttonT);
            buttonT.transform.SetParent(contextMenu.transform);
            Usable.UsableFunc funcT = func.Value;

            buttonT.GetComponent<Button>().onClick.AddListener(() => funcT(player)); // ДА СУКА ДА
            buttonT.transform.Find("Text").GetComponent<Text>().text = func.Key;
        }
    }

    //это для множества объектов было, вот он кажись не нужен
    public void PanelDraw(string objName, Dictionary<string,Usable.UsableFunc> useDict, Player player)
    {
        objectName.text = objName;
        foreach (KeyValuePair<string, Usable.UsableFunc> func in useDict)
        {
            GameObject buttonT = Instantiate(buttonPref);
            buttons.Add(buttonT);
            buttonT.transform.SetParent(contextMenu.transform);
            Usable.UsableFunc funcT = func.Value;
            buttonT.GetComponent<Button>().onClick.AddListener(() => funcT(player)); // ДА СУКА 
            buttonT.transform.Find("Text").GetComponent<Text>().text = func.Key;
        }
    }
}
