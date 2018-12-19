using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	/*  This class is for the boxes in City Trial
		Blue Boxes contain 1 to 4 stat patches, or rarely food. If there is an HP Patch, it's only one.
		Green Boxes contain Quick Fix and special offense patches, rarer than blue and red. (Under Temp)
		Red Boxes contain Copy Patches, and very rarely Dragoon/Hydra Parts.				(Under Temp)
	*/

	// The main type of drop this box will contain.
	public GameObject drops;

	// An array of the various materials the boxes can have.
	public Material[] mats;

	// The boxes' MAX HP.
	public int HP;

	// The boxes' current HP.
	public float curHP;

	// The renderer pf the box.
	private Renderer ren;

	// The players gameObject.
	private GameObject player;

	// The player's animator.
	private Animator playerAnim;

	// The player's machine.
	private GameObject playerMachine;

	// The script for the AirRide machine's main code.
	private AirRide ar;

	// Use this for initialization
	void Start () {
		curHP = HP;
		ren = GetComponent<Renderer> ();
		ren.material = mats [0];
		player = GameObject.FindGameObjectWithTag ("Kirby");
	}
	
	// Update is called once per frame
	void Update () {
		// Slightly damaged box
		if (curHP < (HP / 1.33f))
			ren.material = mats [1];

		// Decent damaged box
		if (curHP < (HP / 2))
			ren.material = mats [2];
		
		// Very damaged box
		if (curHP < (HP / 4))
			ren.material = mats [3];

		// Broken Box
		if (curHP <= 0) {
			spawnStuff ();
			Destroy (this.gameObject);// Break, spawn in 1, 2 or 4 items, shoot them out slightly.
		}
		//curHP--;
		//print ("CurHP" + curHP);
	}

	// If hit from Kirby, reduce HP using an algorithm derived from speed, weight, and offensive abilities of the air Ride.
	// Also if the player is spinning, do some extra damage.
	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Kirby") {
			playerMachine = col.gameObject.GetComponent<KirbyWalk> ().getAirRideGameObject ();
			if (playerMachine == null)
				print ("nope!");
			ar = playerMachine.GetComponent<AirRide> ();
			if (ar == null)
				print ("nope!");
			curHP -= ((ar.acceleration * ar.topSpeed) * (ar.offense * 1.2f) * (ar.weight * 1.05f));
			playerAnim = player.GetComponent<Animator> ();
			if (playerAnim.GetBool ("Spinning"))
				curHP -= 10;
		}

	}


	// When it's destroyed, it will drop some powerups.
	void spawnStuff() {
		int num = Random.Range (0, 9);
		if (num < 3) {// One Drop
			GameObject item = (GameObject)Instantiate (drops, transform.position, transform.rotation);
			item.GetComponent<Rigidbody> ().AddForce (transform.up * 150);
		}
		if (num >= 3 && num < 7) { // 2 Drops
			GameObject item2 = (GameObject)Instantiate (drops, transform.position, transform.rotation);
			item2.GetComponent<Rigidbody> ().AddForce (transform.up * 150);
			item2.GetComponent<Rigidbody> ().AddForce (transform.right * 10);
			GameObject item3 = (GameObject)Instantiate (drops, transform.position, transform.rotation);
			item3.GetComponent<Rigidbody> ().AddForce (transform.up * 150);
			item3.GetComponent<Rigidbody> ().AddForce (-transform.right * 10);
		}

		if (num >= 7) { // 4 Drops
			GameObject item4 = (GameObject)Instantiate (drops, transform.position, transform.rotation);
			item4.GetComponent<Rigidbody> ().AddForce (transform.up * 150);
			item4.GetComponent<Rigidbody> ().AddForce (transform.right * 10);
			GameObject item5 = (GameObject)Instantiate (drops, transform.position, transform.rotation);
			item5.GetComponent<Rigidbody> ().AddForce (transform.up * 150);
			item5.GetComponent<Rigidbody> ().AddForce (-transform.right * 10);
			GameObject item6 = (GameObject)Instantiate (drops, transform.position, transform.rotation);
			item6.GetComponent<Rigidbody> ().AddForce (transform.up * 150);
			item6.GetComponent<Rigidbody> ().AddForce (transform.forward * 10);
			GameObject item7 = (GameObject)Instantiate (drops, transform.position, transform.rotation);
			item7.GetComponent<Rigidbody> ().AddForce (transform.up * 150);
			item7.GetComponent<Rigidbody> ().AddForce (-transform.forward * 10);
		}
	}
}
