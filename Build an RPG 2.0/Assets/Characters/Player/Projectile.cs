using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float projectileSpeed;

	public float damageCaused;

	// Use this for initialization
	void Start () {
		
	}


	private void OnCollisionEnter(Collision other)
	{
		print(other.gameObject + "Hit the ball");

		IDamageable damageableCompenent = other.gameObject.GetComponent<IDamageable>(); // Finds a component on the hit collidergameobject that implements the IDamageable interface

		if (damageableCompenent != null)
		{
			damageableCompenent.TakeDamage(damageCaused);
		}

		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
