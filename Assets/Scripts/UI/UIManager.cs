using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public static UIManager uiManager;
    
    public CanvasRenderer objectUsePanels;
    public CanvasRenderer playerUsePanels;
    public CanvasRenderer optionalPanels;

    public GameObject usePanelPref;
    public GameObject usePanel;
    public List<GameObject> usePanels = new List<GameObject>();

    public GameObject useHandsPanel;

    public GameObject optionalUsePanelPref;
    public GameObject optionalUsePanel;
    public List<GameObject> optionalUsePanels = new List<GameObject>();


    public Text timer;
    public Text rating;

    // IN WORLD UI
    //mouse over usable object text
    public Canvas inWorldCanvas;
    public Text overText;


    // Use this for initialization
    void Start () {
        uiManager = this;
	}

    void Update()
    {
        timer.text= Mathf.Floor(JointManager.timeGlobal/60).ToString()+":"
            + Mathf.RoundToInt(JointManager.timeGlobal % 60).ToString();
        rating.text = JointManager.cleanRating.ToString("0.##");
    }

    public void ContextDraw(Usable usable, Player player)
    {
            usePanel = Instantiate(usePanelPref);
            usePanel.GetComponent<UsePanel>().PanelDraw(usable, player);
            usePanel.transform.SetParent(objectUsePanels.transform);
            usePanels.Add(usePanel);

    }


    public void ContextHandsDraw(Player player)
    {
        if (player.itemPlace.items[0] != null)
        {
            //руки
            usePanel = Instantiate(usePanelPref);
            //Debug.Log(player.itemPlace.items[0].GetComponent<Usable>().ShowUsable());
            usePanel.GetComponent<UsePanel>().PanelDraw(player.itemPlace.items[0].GetComponent<Usable>(), player);
            usePanel.transform.SetParent(playerUsePanels.transform);
            usePanel.transform.SetAsFirstSibling();
            useHandsPanel=usePanel;
        }


    }

    public void ContextClear()
    {
        foreach (GameObject usePanelT in usePanels)
            Destroy(usePanelT);

        usePanels.Clear();
    }
    public void ContextHandsClear()
    {
        Destroy(useHandsPanel);

        //useHandsPanels.Clear();
    }

    public void ContextRedraw(Player player)
    {
        ContextClear();
        ContextHandsClear();
        //OptionalClear();
        ContextHandsDraw(player);
        if (player.triggerObj != null)
        {
            ContextDraw(player.triggerObj.GetComponent<Usable>(), player);
            
        }
    }

    public void OptionalDraw(Placable placable, ItemPlace itemPlace)
    {
        int x = 0;
        for (int i = 0; i < itemPlace.placeCount; i++)
        {
            if (itemPlace.hasItemPlaceds[i])
                x++;
        }

        if (x != itemPlace.placeCount)
        {
            optionalUsePanel = Instantiate(optionalUsePanelPref);
            optionalUsePanel.GetComponent<OptionalPanel>().PanelDraw(placable, itemPlace);
            optionalUsePanel.transform.SetParent(optionalPanels.transform);
            optionalUsePanels.Add(optionalUsePanel);
        }


        //проверка на предметы 
        for (int i = 0; i < itemPlace.placeCount; i++)
        {
            if (itemPlace.hasItemPlaceds[i] && itemPlace.items[i].GetComponent<ItemPlace>() != null)
            {
                OptionalDraw(placable, itemPlace.items[i].GetComponent<ItemPlace>());
            }
        }

        
        
    }

    public void OptionalClear()
    {
        foreach (GameObject optionalPanelT in optionalUsePanels)
          Destroy(optionalPanelT);

        //usePanels.Clear();
    }

    //over text
    public void ShowOverText(GameObject obj)
    {
        overText.text = obj.name;
        overText.transform.position = obj.transform.position + new Vector3(0, 2, 0);

        //чтобы всегда текст смотрел в камеру
        Vector3 v = Camera.main.transform.position - overText.transform.position;
        //v.x = v.z = 0.0f;
        overText.transform.LookAt(Camera.main.transform.position - v);
        transform.Rotate(0, 180, 0);
    }

    public void HideOverText()
    {
        overText.text = "";
    }
}
