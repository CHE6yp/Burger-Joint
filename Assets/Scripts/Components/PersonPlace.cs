using UnityEngine;
using System.Collections;

public class PersonPlace : MonoBehaviour {

    public GameObject place;
    public GameObject person;//?

    public GameObject place2;


    // Use this for initialization
    void Start () {
        place = transform.Find("PersonPlace").gameObject;
    }
	
}
