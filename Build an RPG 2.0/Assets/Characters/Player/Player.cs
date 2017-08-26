using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CameraRaycaster))]


public class Player : MonoBehaviour, IDamageable {


	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] GameObject currentTarget;
	CameraRaycaster cameraRaycaster;
	[SerializeField] const int enemyLayer = 9;


	[SerializeField] float playerDamage = 10f;
	[SerializeField] float minTimeBetweenHits = 0.5f;
	[SerializeField] float maxAttackRange = 2f;



	private float lastHitTime = 0f;
	private float currentHealthPoints;

	// Use this for initialization
	void Start () {
		cameraRaycaster = FindObjectOfType<CameraRaycaster>();
		cameraRaycaster.notifyMouseClickObservers += AssignATarget;
		currentHealthPoints = maxHealthPoints;
		
	}

	void AssignATarget (RaycastHit raycastHit, int layerHit)
	{

		if (layerHit == enemyLayer)
		{
			var enemy = raycastHit.collider.gameObject;
			currentTarget = enemy;
			Vector3 faceTheEnemy = (enemy.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(faceTheEnemy), 0.2f);
			float distanceToTarget = Vector3.Distance(transform.position, enemy.transform.position);



			if ((Time.time - lastHitTime) > minTimeBetweenHits && distanceToTarget <= maxAttackRange)
			{
				
				currentTarget.GetComponent<Enemy>().TakeDamage(playerDamage);
				lastHitTime = Time.time;
			}

			return;
		}

	}

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);

	}


	public float healthAsPercentage
	{
		get
		{
			return currentHealthPoints / maxHealthPoints;
		}
	}
	

}
