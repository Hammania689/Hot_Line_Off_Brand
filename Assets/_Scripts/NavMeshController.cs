using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
	[SerializeField] private Transform desTrans;
	[SerializeField] private Transform[] movementPoints = new Transform[3];
	
	private int index;
	private NavMeshAgent navAgent;

	
	// Use this for initialization
	void Start ()
	{
		navAgent = gameObject.GetComponent<NavMeshAgent>();
		index = Mathf.RoundToInt(Random.Range(0,3));
		navAgent.SetDestination(movementPoints[index].transform.position);
		navAgent.destination = movementPoints[index].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Patrol();
	}

	// Returns True if Enemy has yet to arrive to current desTrans
	private bool isInTransit(int i)
	{
		if (Vector3.Distance(transform.position,navAgent.destination) > 2) return true;
		return false;
	}

	/*
	// Checks to see whether or not the player is nearby
	private bool isPlayerNearby()
	{
		float disFromPlayer = Vector3.Distance(this.transform.position, playerTrans.position);

		if (disFromPlayer <= attentionRange)
		{ attentionRange += 2; return true; }
		return false;
	}
	 */

	// Randomly Iterates through Array of Travel Positions
	private void Patrol()
	{
        /*
	    for(int curr = 0; curr < movementPoints.Length; curr++)
	    {

	        if (!isInTransit(curr))
	        {
	            navAgent.SetDestination(movementPoints[curr].transform.position);
	            navAgent.destination = movementPoints[curr].transform.position;
	        }
	    }
         */
        
	    if (!isInTransit(index))
	    {
	        Debug.Log("Changing Pos to Node " + index);
	        index = Mathf.RoundToInt(Random.Range(0, 3));
	        navAgent.SetDestination(movementPoints[index].transform.position);
	        navAgent.destination = movementPoints[index].transform.position;
	    }
	    else
	        index = 0;
	}
}
