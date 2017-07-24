using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private float travelSpeed = 3.4f;
	[SerializeField] private float attentionRange = 6;
	[SerializeField] private Transform[] movementPoints = new Transform[4];
	[SerializeField] private Transform playerTrans;

	private int index;

	// Use this for initialization
	void Start ()
	{
		index = Mathf.RoundToInt(Random.Range(0,3));
		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		Debug.Log(playerTrans.transform.position);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Patrol();
	}

	// Returns True if Enemy has yet to arrive to current desTrans
	private bool isInTransit(int i)
	{
		if (transform.position != movementPoints[i].position) return true;
		return false;
	}

	// Checks to see whether or not the player is nearby
	private bool isPlayerNearby()
	{
	   float disFromPlayer = Vector3.Distance(this.transform.position, playerTrans.position);

		if (disFromPlayer <= attentionRange)
		{attentionRange += 2; return true;}
		return false;
	}

	// Randomly Iterates through Array of Travel Positions
	private void Patrol()
	{
		print("Current Destination : " + index);
		if (isPlayerNearby() == false)
			if (isInTransit(index) == true)
				this.transform.position = Vector3.MoveTowards(transform.position, movementPoints[index].position,
					travelSpeed * Time.deltaTime);
			else
				index = Mathf.RoundToInt(Random.Range(0, 3));
		else
			this.transform.position = Vector3.MoveTowards(transform.position, playerTrans.position, travelSpeed * Time.deltaTime * 1.4f);

	}
}
