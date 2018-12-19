using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPatch : MonoBehaviour {

	// Keeps track of the type of stat he got using an ID
	private int statType;

	// This passes that ID along to another function, where I didn't keep consistent ID's...
	private int statID; 
	// 0 - 9 are 
	/* All Up
	 * Boost Up
	 * Charge Up
	 * Defense Up
	 * Glide Up
	 * HP UP
	 * Offense Up
	 * TopSpeed Up
	 * Turn Up
	 * Weight Up
	 */

	// An array of sprites used to display which stat he picked up.
	public Sprite[] pads;

	// The camera.
	private GameObject cam;



	// Use this for initialization
	// On Instanciation, it will randomly assign a stat type and assign it's variables.
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");

		statType = Random.Range (1, 93);
		if (statType >= 1 && statType <= 11) {
			this.GetComponent<Image> ().sprite = pads [0];
			statID = 7;
		}
		if (statType >= 12 && statType <= 22) {
			this.GetComponent<Image> ().sprite = pads [1];
			statID = 8;
		}
		if (statType >= 23 && statType <= 33) {
			this.GetComponent<Image> ().sprite = pads [2];
			statID = 9;
			}
		if (statType >= 34 && statType <= 44) {
			this.GetComponent<Image> ().sprite = pads [3];
			statID = 4;
		}
		if (statType >= 45 && statType <= 55) {
			this.GetComponent<Image> ().sprite = pads [4];
			statID = 2;
		}
		if (statType >= 56 && statType <= 66) {
			this.GetComponent<Image> ().sprite = pads [5];
			statID = 6;
		}
		if (statType >= 67 && statType <= 77) {
			this.GetComponent<Image> ().sprite = pads [6];
			statID = 3;
		}
		if (statType >= 78 && statType <= 88) {
			this.GetComponent<Image> ().sprite = pads [7];
			statID = 1;
		}
		if (statType >= 89 && statType <= 91) {
			this.GetComponent<Image> ().sprite = pads [8];
			statID = 5;
		}
		if (statType == 92) {
			this.GetComponent<Image> ().sprite = pads [9];
			statID = 0;
		}

	}
	
	// Keep the stats facing the Camera, to give a false sense of 3D imagery to the 2D sprites.
	void Update () {
		this.transform.LookAt(cam.transform.position) ;
	}

	// Returns stat ID
	public int getStatID() {
		return statID;
	}

}
