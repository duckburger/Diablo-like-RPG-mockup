using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CameraRaycaster))]

public class CursorAffordance : MonoBehaviour {


	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D attackCursor = null;
	[SerializeField] Texture2D unknownCursor = null;
 	[SerializeField] Vector2 cursorHotSpot = new Vector2(0, 0);

	// TODO serialize properly
	[SerializeField] const int walkableLayer = 8;
	[SerializeField] const int enemyLayer = 9;
	

	CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster>();
		cameraRaycaster.notifyLayerChangeObservers += WhenLayerChanges; // TODO maybe deregister when the game exits all scenes

		
		
	}
	

	void WhenLayerChanges (int newLayer) {

		switch (newLayer)
		{

			case  walkableLayer:
				Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
				break;

			case enemyLayer:
				Cursor.SetCursor(attackCursor, cursorHotSpot, CursorMode.Auto);
				break;

			default:
				Debug.LogError ("An impossible scenraio. Don't know what cursor to show");
				Cursor.SetCursor(unknownCursor, cursorHotSpot, CursorMode.Auto);
				break;

		}

		}
	}
