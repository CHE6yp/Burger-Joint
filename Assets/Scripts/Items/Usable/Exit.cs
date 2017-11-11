using UnityEngine;
using System.Collections;

[SelectionBase]
[RequireComponent (typeof (PersonPlace))]
[RequireComponent(typeof(Usable))]
public class Exit : MonoBehaviour {

    public Usable usable;

    public GameObject customerGO;

    public PersonPlace hasPersonPlace;


    // Use this for initialization
    void Start () {
        hasPersonPlace = GetComponent<PersonPlace>();

        usable = GetComponent<Usable>();

        usable.useDict.Add("Exit", ExitJoint);
    }
	
	public void ExitJoint(Player player)
    {
        if (player.transform.tag == "Customer")
            Destroy(player.gameObject);
    }

    public void SpawnCustomer()
    {
        Instantiate(customerGO);
    }
}
