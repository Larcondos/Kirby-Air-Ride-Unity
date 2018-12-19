using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatNotifications : MonoBehaviour {

	// Basic Image for the canvas.
	public Image statCanvas;

	// Array of sprites that the image may be.
	public Sprite[] statImage;

	// A timer used to keep track of how long the message is displayed.
	private int timer;

	// If the timer is currently on or not.
	private bool startTimer;


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

	// Use this for initialization
	void Start () {
		statCanvas.enabled = false;
	}
	
	// Update is called once per frame
	// When a pickup is gathered, it displays the message for 200 frames (a few seconds).
	void Update () {
		if (startTimer) {
			timer++;

		}
		if (timer > 200) {
			statCanvas.enabled = false;
			//print ("disable!");
			startTimer = false;
			timer = 0;
		}
	}


	public void SetStat(int statPick) {
		statCanvas.enabled = true;
		statCanvas.sprite = statImage [statPick];
		if (startTimer)
			timer = 0;
		else
			startTimer = true;

	}
}
