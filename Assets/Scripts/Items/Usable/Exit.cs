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
        GetComponent<Usable>().use = ExitJoint;

        usable.useDict.Add("Exit", ExitJoint);
        /*
        GetComponent<Usable>().use = ExitJoint;
        GetComponent<Usable>().useArray = new Usable.UsableFunc[1];
        GetComponent<Usable>().useArray[0] = ExitJoint;
        */
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
