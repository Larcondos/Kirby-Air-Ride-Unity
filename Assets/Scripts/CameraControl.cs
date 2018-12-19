using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraControl : MonoBehaviour {

	// Gameobject for the player.
	private GameObject player;

	// The distance to follow Kirby at. 4 is default.
	public float distance;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Kirby");
	}

	// Use lateUpdate incase any movement is done before moving the camera. 
	// Keeps rotation consistent, and follows the player at set distance.
	void LateUpdate ()
	{
		transform.rotation = Quaternion.identity;
		// Keep camera a specified distance away from player
		transform.position = player.transform.position - transform.forward * distance;
		transform.position += transform.up * 1.5f;
		//transform.LookAt (player.transform);
	}
}
	