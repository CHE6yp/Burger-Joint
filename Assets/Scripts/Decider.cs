using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decider : MonoBehaviour {


    public int probability = 25;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Customer") 
        {
            if (!other.GetComponent<BotAI>().deciderFlag && Random.Range(0, 100) < probability)
                {
                other.GetComponent<BotAI>().Rate();
                other.GetComponent<BotAI>().state = BotAI.BotState.Deciding;
            }
            other.GetComponent<BotAI>().deciderFlag = true;
        }
        
    }

}
