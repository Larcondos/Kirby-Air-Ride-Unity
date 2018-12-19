using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveStarEffects : MonoBehaviour {

	// Particle effect game objects.
	public GameObject part1, part2, part3, part4;

	// Script for the airRides' main info,.
	private AirRide ar;

	// THe original turn rate of the vehicle.
	private float origTurn;

	void Start() {
		ar = gameObject.GetComponent<AirRide> ();

			
	}


	// This is used to adjust for a special case of turning on the Swerve Star. I should probably make a new script for this, but this works for now.
	void Update() {
		ar.turn = origTurn;
		if (ar.charge != 100)
			origTurn = ar.turn;
		
		if (ar.charge >= 100)
			ar.turn = 5;
		

	}

	// Turns on particle effects.
	public void StartParticles() {
		part1.gameObject.SetActive (true);
		part2.gameObject.SetActive (true);
		part3.gameObject.SetActive (true);
		part4.gameObject.SetActive (true);
		GetComponent<Animator> ().SetBool ("Ridden", true);

	}

	// Turns off particle effects.
	public void StopParticles() {
		part1.gameObject.SetActive (false);
		part2.gameObject.SetActive (false);
		part3.gameObject.SetActive (false);
		part4.gameObject.SetActive (false);
		GetComponent<Animator> ().SetBool ("Ridden", false);
	}
}
