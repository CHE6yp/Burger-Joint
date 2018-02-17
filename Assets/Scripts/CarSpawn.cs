using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour {

    public int number;
    public bool active;
    public float time = 3;
    public float interval = 0;
    public GameObject[] cars;
    public Transform where;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {

            time += Time.deltaTime;
            if (time >= interval)
            {
                SpawnCar();
                time = 0;
                interval = Random.Range(1, 4);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car" && other.GetComponent<Car>().whereSpawned != number)
            Destroy(other.gameObject);
    }

    void SpawnCar()
    {
        GameObject car = Instantiate(cars[Random.Range(0, cars.Length)], transform.position, transform.rotation);
        car.GetComponent<Car>().dest = where;
        car.GetComponent<Car>().whereSpawned = number;

    }

}
