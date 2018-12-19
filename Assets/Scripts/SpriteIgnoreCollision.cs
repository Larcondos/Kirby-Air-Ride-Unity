using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteIgnoreCollision : MonoBehaviour {

	// The script for accessing Kirby's stats page.
	private KirbyStats ks;

	// The Gameobject of Kirby
	private GameObject player;

	// The gameobject of the statSprite.
	public GameObject statSprite;

	// The script for accessing StatPatch info.
	private StatPatch sp;

	// The script for accessing notifications about stat info.
	private StatNotifications sn;

	// The script that contains most information for Air Ride machines.
	private AirRide ar;


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

	// On start is assigns scripts to their variables, and ignores collision with other sprites (On layer 9).
	// Additionally, Kirby can only pick up sprites on his machine, not while walking.
	void Start() {
		player = GameObject.FindGameObjectWithTag ("Kirby");
		ks = player.GetComponent<KirbyStats> ();
		sp = statSprite.GetComponent<StatPatch> ();
		sn = player.GetComponent<StatNotifications> ();


		Physics.IgnoreLayerCollision(9, 9);
		Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Kirby").GetComponent<Collider>());
	}

	void Update() {
	}

	// When triggered, gives stats based on what type of stat it was, and destroys itself.
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "KirbyMachine")
		{
			// Updates Kirby's Stat Page
			if (sp.getStatID () == 0)
				ks.allPatch ();
			if (sp.getStatID () == 1)
				ks.boostPatch ();
			if (sp.getStatID () == 2)
				ks.chargePatch();
			if (sp.getStatID () == 3)
				ks.defensePatch ();
			if (sp.getStatID () == 4)
				ks.glidePatch ();
			if (sp.getStatID () == 5)
				ks.HPPatch ();
			if (sp.getStatID () == 6)
				ks.offensePatch ();
			if (sp.getStatID () == 7)
				ks.topSpeedPatch ();
			if (sp.getStatID () == 8)
				ks.turnPatch ();
			if (sp.getStatID () == 9)
				ks.weightPatch ();

			sn.SetStat(sp.getStatID());
			ar = col.gameObject.GetComponent<AirRide> ();
			ar.statBoost (sp.getStatID ());
			Destroy(gameObject);
		}
	}

}

