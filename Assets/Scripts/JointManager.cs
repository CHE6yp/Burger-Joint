using UnityEngine;
using System.Collections;

public class JointManager : MonoBehaviour {

    public static JointManager jm;
    public static GameObject[] tables;
    public static GameObject[] registers;
    public static GameObject[] exits;
    public static GameObject[] garbages;
    public GameObject spawn1;
    public GameObject spawn2;

    public GameObject spawn3;
    public GameObject spawn4;

    public GameObject customer;
    public int botCount = 1;
    public int currentBotMax = 50;
    public static int currentBotCount = 0;

    //autospawning bots
    public float minInter = 0.1f;
    public float maxInter = 3f;
    public float interval = 10f;
    public float time = 0;

    //STATS
    public static float timeGlobal = 0;

    private static float cleanTables = 0;
    private static float ratingCount = 0;
    public static float cleanRating
    {
        get { if (ratingCount != 0) return cleanTables / ratingCount;
            else return 0;
        }
        set { ratingCount++; cleanTables += value;
            //Debug.Log(ratingCount + "/" + cleanTables+"/"+cleanRating); 
        }
    }


    // Use this for initialization
    void Start () {
        jm = this;

        registers = GameObject.FindGameObjectsWithTag("Register");
        tables = GameObject.FindGameObjectsWithTag("Table");
        
        exits = GameObject.FindGameObjectsWithTag("Exit");
        garbages = GameObject.FindGameObjectsWithTag("Garbage");
        time = 0f;
        timeGlobal = 0f;
        
    }

    void Update()
    {
        timeGlobal += Time.deltaTime;


        if (currentBotCount < currentBotMax)
        {
            time += Time.deltaTime;
            if (time >= interval)
            {
                SpawnCustomer();
                time = 0;
                interval = Random.Range(minInter, maxInter);
            }
                
        }
    }

    public static GameObject RandomTable()
    {
        return tables[Random.Range(0, tables.Length)];
    }

    public static GameObject RandomRegister()
    {
        return registers[Random.Range(0, registers.Length)];
    }

    public static GameObject RandomExit()
    {
        return exits[Random.Range(0, exits.Length)];
    }

    public static GameObject RandomGarbage()
    {
        return garbages[Random.Range(0, garbages.Length)];
    }

    [ContextMenu("Spawn Bot")]
    public void SpawnCustomer()
    {
        //GameObject exitT = RandomExit().GetComponent<PersonPlace>().place;
        //var bot = Instantiate(customer, exitT.transform.position,exitT.transform.rotation);
        GameObject bot;

        //НАДО ПЕРЕПИСАТЬ ПОЛУЧШЕ
        if (Random.Range(0, 2) == 0)
        {
            bot = Instantiate(customer, spawn1.transform.position, spawn1.transform.rotation);
            bot.GetComponent<BotAI>().whereSpawned = 1;
            //проблема на маке, на винде эта строка не нужна
            bot.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(spawn1.transform.position);
        }
        else
        {
            bot = Instantiate(customer, spawn2.transform.position, spawn2.transform.rotation);
            bot.GetComponent<BotAI>().whereSpawned = 2;
            //проблема на маке, на винде эта строка не нужна
            bot.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(spawn2.transform.position);
        }
        bot.name = "Bot " + botCount.ToString();
        botCount++;

        if (Random.Range(0, 2) == 0)
        {
            bot = Instantiate(customer, spawn3.transform.position, spawn3.transform.rotation);
            bot.GetComponent<BotAI>().whereSpawned = 3;
            //проблема на маке, на винде эта строка не нужна
            bot.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(spawn3.transform.position);
        }
        else
        {
            bot = Instantiate(customer, spawn4.transform.position, spawn4.transform.rotation);
            bot.GetComponent<BotAI>().whereSpawned = 4;
            //проблема на маке, на винде эта строка не нужна
            bot.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(spawn4.transform.position);
        }
        bot.name = "Bot " + botCount.ToString();
        botCount++;
    }
}
