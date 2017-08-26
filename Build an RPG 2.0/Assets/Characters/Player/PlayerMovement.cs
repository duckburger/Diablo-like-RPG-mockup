using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;

[RequireComponent(typeof (ThirdPersonCharacter))]
[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (AICharacterControl))]

public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster = null;
	AICharacterControl aiCharacterControl = null;
	GameObject walkTarget = null;

	// TODO serialize properly
	[SerializeField] const int walkableLayer = 8;
	[SerializeField] const int enemyLayer = 9;





	private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>(); // Get the CameraRaycaster component from the main camera
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
		aiCharacterControl = GetComponent<AICharacterControl>();

		cameraRaycaster.notifyMouseClickObservers += ProcessPathfindingMovement;
		walkTarget = new GameObject();
    }


	void ProcessPathfindingMovement (RaycastHit raycastHit, int layerHit)
	{

		switch (layerHit)
		{
			case enemyLayer:
				GameObject enemy = raycastHit.collider.gameObject;
				aiCharacterControl.SetTarget(enemy.transform);
					break;

			case walkableLayer:
				walkTarget.transform.position = raycastHit.point;
				aiCharacterControl.SetTarget(walkTarget.transform);
				break;

			default:
				Debug.LogError("Don't know how to handle this click");
				return;
				




		}
	}

	// TODO make this work again (get called)
	private void ProcessGamepadMovement()
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		// calculate camera relative direction to move:
		Vector3 CameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 Movement = v * CameraForward + h * Camera.main.transform.right;

		thirdPersonCharacter.Move(Movement, false, false);
	}

	

	
}

