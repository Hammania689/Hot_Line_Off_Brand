using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
	[SerializeField] private Transform[] movementPoints = new Transform[3];
	
	private int index;
	private int length;
	private NavMeshAgent navAgent;

	
	// Use this for initialization
	void Start()
	{
		navAgent = gameObject.GetComponent<NavMeshAgent>();
		index = 0;
		length = movementPoints.Length - 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Patrol();
	}

	// Returns True if Enemy has yet to arrive to current desTrans
	private bool isAtDestination()
	{
		if(Vector3.Distance(transform.position, navAgent.destination) < 2)
			return true;
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
		Debug.Log("Travelling to point : " + index);

		if(isAtDestination())
		{
			index++;
			if (index == length)
				index = 0;
			else if (index > length)
				return;
			navAgent.destination = movementPoints[index].position;
		}
	}
}
