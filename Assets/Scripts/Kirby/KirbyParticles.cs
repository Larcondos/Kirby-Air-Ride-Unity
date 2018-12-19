using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyParticles : MonoBehaviour {

	// The gameobjects for Kirby's charge particles.
	public GameObject chargeParticles;

	// The gameobject for Kirby's running particles.
	public GameObject runningParticles;

	// Kirby's animator.
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	// If I'm running, play these particles, If I'm charging do these ones.
	void Update () {
		if (anim.GetBool("Charging"))
			chargeParticles.SetActive(true);
		else
			chargeParticles.SetActive(false);

		if (anim.GetBool("Running"))
			runningParticles.SetActive(true);
		else
			runningParticles.SetActive(false);
	}
}
