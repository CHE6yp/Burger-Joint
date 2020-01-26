using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI : MonoBehaviour {

    public Player player;

    public UnityEngine.AI.NavMeshAgent navigator;
    public GameObject destination; //То, куда бот стремится. Тобишь касса, стол итд.
    public GameObject destinationPlace; //То, куда ему надо встать.
    protected Vector3 gizmoOffset = new Vector3(0, 1, 0);

    // Use this for initialization
    void Start () {
		//test
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public abstract void StateMachine();

    public void SetDestination(Vector3 position)
    {
        navigator.destination = position;
    }

    protected void SetDestination(GameObject dest)
    {
        destination = dest;
        if (dest.GetComponent<PersonPlace>() != null)
        {
            destinationPlace = dest.GetComponent<PersonPlace>().place;
        }
        //navigator.destination = destinationPlace.transform.position;
        navigator.destination = destinationPlace.transform.position;
    }

    protected void SetDestinationCommon(GameObject dest)
    {
        destination = dest;
        if (dest.GetComponent<PersonPlace>() != null)
        {
            destinationPlace = dest.GetComponent<PersonPlace>().place;

        }

    }

    protected void SetDestination2(GameObject dest)
    {
        destination = dest;
        if (dest.GetComponent<PersonPlace>() != null)
        {
            destinationPlace = dest.GetComponent<PersonPlace>().place2;

        }
        navigator.destination = destinationPlace.transform.position;
    }

    protected bool ReachedDest()
    {
        
        if (!navigator.pathPending)
        {
            if (navigator.remainingDistance <= 0.3f)
            {
                /*
                if (!navigator.hasPath || navigator.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
                else return false;
                */
                return true;
            }
            else return false;
        }
        else return false;

    }

    //чужое
    protected void RotateTowards(Transform target)
    {

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

    }





    protected void OnDrawGizmos()
    {
        if (destinationPlace != null)
        //if (navigator.destination != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(navigator.destination + gizmoOffset, 0.5f);
            Gizmos.DrawRay(player.transform.position, navigator.destination-player.transform.position);
        }

    }
}
