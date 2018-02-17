using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {

    int time;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate () {
        time++;
        if (time > 500)
            Destroy(gameObject);
	}
}
