using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour {

    public int whereSpawned;
    public Transform dest;
    public int lifeSpan = 300;
    public int time;
    public GameObject blood;
	// Use this for initialization
	void Start () {
        //GetComponent<NavMeshAgent>().destination = dest.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.localPosition += new Vector3(0, 0, (transform.eulerAngles.y == 0.0f)?1:-1);
        time++;
        if (time > lifeSpan)
            Destroy(gameObject);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Customer")
        {
            Instantiate(blood, new Vector3(other.transform.position.x, 0, other.transform.position.z), Quaternion.Euler(90,0,0));
            Destroy(other.gameObject);
        }
    }
}
