using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirRide : MonoBehaviour {

	//Air Ride Stats
	public float origmaxHP, maxHP, HP, 
				 offense, defense, 
				 charge, chargeAmt, boost, 
				 origtopSpeed, topSpeed, turn, 
				 acceleration, accelerationRate,
				 weight, glide; 
	public GameObject kirby; 	// My player!
	private BoxCollider col; 	// My box collider.

	// The location Kirby will sit at.
	public GameObject sitSpot;

	// Access Kirby's stats page.
	private KirbyStats ks;

	// Keeps track of original rotation.
	private Quaternion origRotation;

	// Is this machine flying or not?
	public bool isGliding = false;

	// Use this for initialization
	void Start () {
		kirby = GameObject.FindGameObjectWithTag ("Kirby");
		origRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		// Air Ride is only doing stuff if kirby is a rider!
		if (transform.parent != null && transform.parent.tag == "Kirby" && ks == null)
		{
			ks = kirby.GetComponent<KirbyStats> ();
			calculate ();
		}
			
	}

	#region Patch Calculations

	// Used for calculating the Air Rides' stuff when Kirby hops on a NEW ride.
	private void calculate () {
		for (int i = 0; i < ks.offense; i++)
			offensePatch ();
		for (int i = 0; i < ks.defense; i++)
			defensePatch ();
		for (int i = 0; i < ks.charge; i++)
			chargePatch ();
		for (int i = 0; i < ks.turn; i++)
			turnPatch ();
		for (int i = 0; i < ks.topSpeed; i++)
			topSpeedPatch ();
		for (int i = 0; i < ks.boost; i++)
			boostPatch ();
		for (int i = 0; i < ks.weight; i++)
			weightPatch ();
		for (int i = 0; i < ks.glide; i++)
			glidePatch ();
		for (int i = 0; i < ks.HP; i++)
			HPPatch ();
		
		
	}
		
	public void HPPatch() {
		//HP is boosted by 18.75%
		float healamt = maxHP;
		maxHP += (origmaxHP * 0.1875f);
		HP += maxHP - healamt;
	}

	public void offensePatch() {
		offense++;
	}

	public void defensePatch() {
		defense++;
	}

	public void chargePatch() {
		// Speed charging time by 5%.
		chargeAmt += (chargeAmt * 0.05f);
	}

	public void turnPatch() {
		// Increase Turn Abilities by 4%. Too many turns is bad...
		turn += (turn * 0.04f);
	}

	public void topSpeedPatch() {
		// Boost by ~3.8%
		topSpeed += (origtopSpeed * 0.0375f);
	}

	public void boostPatch() {
		boost++;
	}

	public void weightPatch() {
		weight++;
	}

	public void glidePatch() {
		glide++;
	}

	// Boost all stats with this patch.
	public void allPatch() {
		offensePatch ();
		defensePatch ();
		chargePatch ();
		turnPatch ();
		topSpeedPatch ();
		boostPatch ();
		weightPatch ();
		glidePatch ();
		HPPatch ();
	}

	public void statBoost(int sp) {
		//print ("called!");
	if (sp == 0)
		allPatch ();
	if (sp == 1)
		boostPatch ();
	if (sp == 2)
		chargePatch();
	if (sp == 3)
		defensePatch ();
	if (sp == 4)
		glidePatch ();
	if (sp == 5)
		HPPatch ();
	if (sp == 6)
		offensePatch ();
	if (sp == 7)
		topSpeedPatch ();
	if (sp == 8)
		turnPatch ();
	if (sp == 9)
		weightPatch ();
	}

	#endregion Patch Calculations

	public Vector3 getSitHeight() {
		return sitSpot.transform.position;
	}
	public void resetRotation() {
		transform.rotation = origRotation;
	}

	// If on contact with the floor, you aren't gliding.
	void OnCollisionEnter(Collision theCollision)
	{
		if (theCollision.gameObject.tag != "Floor") {
			isGliding = true;
		} else
			isGliding = false;
	}
}
