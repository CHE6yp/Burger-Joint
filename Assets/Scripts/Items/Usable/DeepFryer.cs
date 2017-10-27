using UnityEngine;
using System.Collections;

public class DeepFryer : MonoBehaviour {

    public Usable usable;
    public ItemPlace itemPlace;

    public bool heatIsOn;
    
    public float temperature = 100f;
    public float heatMax = 200f;
    public float heatMin = 100f;
    public TextMesh heatText;


    public bool timerIsOn;
    public float timer = 8.999f;
    public float timerCurrent = 8.999f;
    public TextMesh timerText;

    

	// Use this for initialization
	void Awake () {
        usable = GetComponent<Usable>();

        usable.useDict.Add("Heat", SetHeat);
        usable.useDict.Add("Timer", SetTimer);

    }

    void Update()
    {
        CountTimer();
        CountHeat();
        ShowHeat();
        Cook();


    }

    void Cook()
    {
        if (itemPlace.items[0] != null && itemPlace.items[0].name == "Basket")
        {

            for (int i = 0; i < itemPlace.items[0].GetComponent<ItemPlace>().items.Length; i++)
            {
                if (itemPlace.items[0].GetComponent<ItemPlace>().items[i] != null
                    && itemPlace.items[0].GetComponent<ItemPlace>().items[i].name == "Meat")

                    itemPlace.items[0].GetComponent<ItemPlace>().items[i].GetComponent<Meat>().Cook(temperature * Time.deltaTime);
            }
        }
    }

    public void SetHeat(Player player)
    {
        if (!heatIsOn)
        {
            heatText.color = Color.green;
            heatIsOn = true;
        }
        else
        {
            heatIsOn = false;
            heatText.color = Color.red;
            //heatText.text = "OFF: " + Mathf.Floor(temperature).ToString();
        }
    }

    void ShowHeat()
    {
        if (heatIsOn)
        {
            heatText.text = "ON: " + Mathf.Floor(temperature).ToString();
        }
        else
            heatText.text = "OFF: " + Mathf.Floor(temperature).ToString();
    }

    void CountHeat()
    {
        if (heatIsOn&&temperature<heatMax)
        {
            temperature += Time.deltaTime;
        }
        if (!heatIsOn && temperature > heatMin)
        {
            temperature -= Time.deltaTime;
        }
        if (temperature > heatMax)
            temperature = heatMax;
        if (temperature < heatMin)
            temperature = heatMin;
    }

    public void SetTimer(Player player)
    {
        if (!timerIsOn)
        {
            
            timerIsOn = true;
        } else
        {
            timerIsOn = false;
            timerText.color = Color.white;
            timerCurrent = timer;
        }
    }

    void CountTimer()
    {
        if (timerIsOn)
        {
            timerCurrent -= Time.deltaTime;
            
            
            if (timerCurrent < 0.5f)
            {
                timerCurrent = 0;
                timerText.color = Color.red;
            }
            
        }
        timerText.text = Mathf.Floor(timerCurrent).ToString();
    }

	
}
