using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {


	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float chaseTriggerRadius = 5f;
	[SerializeField] float attackTriggerRadius = 1.5f;
	[SerializeField] float damagePerShot = 9f;
	[SerializeField] float secondsBetweenShots = 0.7f;
	[SerializeField] Vector3 aimOffeset = new Vector3(0,1,0);



	[SerializeField] GameObject projectileToUse;
	[SerializeField] GameObject projectileSocket;


	private bool isAttacking = false;
	private float currentHealthPoints;
	private float distanceBetweenTheEnemyAndPlayer;
	ThirdPersonCharacter thirdPersonCharacter;
	AICharacterControl aiCharacterControl;
	GameObject player;


	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");
		aiCharacterControl = GetComponent<AICharacterControl>();
		currentHealthPoints = maxHealthPoints;
	}

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
		if (currentHealthPoints <= 0)
		{
			Destroy(gameObject);
		}
	}


	public float healthAsPercentage
	{
		get
		{
			return currentHealthPoints / maxHealthPoints;
		}
	}
	
	// Update is called once per frame
	void Update () {

		distanceBetweenTheEnemyAndPlayer = Vector3.Distance(transform.position, player.transform.position);

		if (distanceBetweenTheEnemyAndPlayer <= chaseTriggerRadius)
		{
			aiCharacterControl.SetTarget(player.transform);
		} else {

			aiCharacterControl.SetTarget(transform);
		}

		if (distanceBetweenTheEnemyAndPlayer <= attackTriggerRadius && !isAttacking)
		{
			

			isAttacking = true;
			InvokeRepeating("SpawnProjectile", 0, secondsBetweenShots);
		}

		if (distanceBetweenTheEnemyAndPlayer > attackTriggerRadius)
		{
			isAttacking = false;
			CancelInvoke();
		}

	}

	private void SpawnProjectile()
	{
		GameObject projectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
		
		Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
		projectileRigidbody.velocity = (player.transform.position + aimOffeset - projectileSocket.transform.position).normalized * projectile.GetComponent<Projectile>().projectileSpeed;

		projectile.GetComponent<Projectile>().damageCaused = damagePerShot;
	}

	private void OnDrawGizmos()
	{

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, chaseTriggerRadius);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackTriggerRadius);

	}
}
