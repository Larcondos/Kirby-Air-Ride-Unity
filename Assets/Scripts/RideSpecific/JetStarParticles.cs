using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetStarParticles : MonoBehaviour {

	// Gameobjects for the particle systems.
	public GameObject part1, part2;


	// Turns on particle effects.
	public void StartParticles() {
		part1.gameObject.SetActive (true);
		part2.gameObject.SetActive (true);
	}

	// Turns off particle effects.
	public void StopParticles() {
		part1.gameObject.SetActive (false);
		part2.gameObject.SetActive (false);
	}
}
