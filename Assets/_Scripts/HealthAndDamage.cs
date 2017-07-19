using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthAndDamage : MonoBehaviour
{
	[SerializeField] private float health = 1;
	[SerializeField] private float damage = .2f;

	public Weapon weapon;
	
	// Use this for initialization
	private void Start ()
	{
		health = 1;
	}

	// Update is called once per frame
	private void Update ()
	{
		ShootProjectile();
		if(shouldBeLiving() == false)
			Destroy(this);
	}

	public bool shouldBeLiving()
	{
		return !(health < 0);
	}

	public void DealDamage()
	{
		damage = weapon.weaponDamage;
	}

	void ShootProjectile()
	{
		Rigidbody2D prefab;
		if (Input.GetMouseButtonDown(0))
		{
			// weapon.projectile = Instantiate(prefab, transform.position, Quaternion.identity,
			//   weapon.projectileHolder.transform) as GameObject;
			prefab = Instantiate(weapon.projectile.GetComponent<Rigidbody2D>(),weapon.projectileHolder.transform.position,Quaternion.identity,weapon.projectileHolder.transform) as Rigidbody2D;
			prefab.velocity += new Vector2(PlayerController.cursorPos.x,PlayerController.cursorPos.y).normalized * 15;
			Destroy(prefab.gameObject,5);
		}
	}
}
