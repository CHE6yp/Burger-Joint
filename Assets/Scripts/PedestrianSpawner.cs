using UnityEngine;
using System.Collections;

public class PedestrianSpawner : MonoBehaviour {


    public GameObject pedPrefab;
    public float interval = 15f;
    public float time = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time +=  Time.deltaTime;
        if (time >= interval)
            Spawn();
	}

    void Spawn()
    {
        GameObject ped = Instantiate(pedPrefab);
        ped.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(transform.position);
        
        time = 0;
    }
}
