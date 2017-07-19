using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthAndDamage : MonoBehaviour
{
	[SerializeField] private float health = 1;
	[SerializeField] private float damage = 1;

	private bool isAnEnemy;
	
	// Use this for initialization
	private void Start ()
	{
		isAnEnemy = gameObject.tag == "Enemy";
		health = 1;
	}

	// Update is called once per frame
	private void Update ()
	{
		if (shouldBeLiving() == false)
			Destroy(this.gameObject);
	}

	// Checks to see weither or not this gameObject has health greater than 0
	public bool shouldBeLiving()
	{
		return !(health <= 0);
	}

	// Subtracts from total Health by the given Damage Value
	// Defualt is set to 1
	public void DealDamage(float damageValue)
	{
		health -= damageValue;
	}

	// Controls all the cases of collision events needed to happen when a projectile hits either an Enemy or Player
	private void OnTriggerEnter2D(Collider2D coll)
	{
		//  Projectile hits a Bot
		if (isAnEnemy && coll.gameObject.tag == "Projectile")
			DealDamage(damage);

	}
}
