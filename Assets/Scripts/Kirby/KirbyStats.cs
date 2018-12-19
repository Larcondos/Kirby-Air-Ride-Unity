using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyStats : MonoBehaviour {

	// This class holds all of Kirby's stats, because I don't want this to clutter my other code.
	// Basically a running total (with limits) of the stats Kirby has collected. Limit is 18 in real game.

	//Number of patches collected. Public for debug.
	public float HP, offense, defense, charge, turn, topSpeed, boost, weight, glide; 


	#region Patch Stat Modifiers

	#region Positive
	public void HPPatch() {
		if (HP < 16)
			HP++;
	}

	public void offensePatch() {
		if (offense < 18)
			offense++;
	}

	public void defensePatch() {
		if (defense < 18)
			defense++;
	}

	public void chargePatch() {
		if (charge < 18)
			charge++;
	}

	public void turnPatch() {
		if (turn < 18)
			turn++;
	}

	public void topSpeedPatch() {
		if (topSpeed < 18)
			topSpeed++;
	}

	public void boostPatch() {
		if (boost < 18)
			boost++;
	}

	public void weightPatch() {
		if (weight < 18)
			weight++;
	}

	public void glidePatch() {
		if (glide < 18)
			glide++;
	}
	public void allPatch() {
		glidePatch ();
		turnPatch ();
		weightPatch ();
		boostPatch ();
		topSpeedPatch ();
		chargePatch ();
		defensePatch ();
		offensePatch ();
		HPPatch ();
	}

	#endregion Positive

	#region Negative

	public void HPPatchDown() {
		HP--;
	}

	public void offensePatchDown() {
		if (offense > -14)
			offense--;
	}

	public void defensePatchDown() {
		if (defense > -14)
			defense--;
	}

	public void chargePatchDown() {
		if (charge > -14)
			charge--;
	}

	public void turnPatchDown() {
		if (turn > -14)
			turn--;
	}

	public void topSpeedPatchDown() {
		if (topSpeed > -14)
			topSpeed--;
	}

	public void boostPatchDown() {
		if (boost > -14)
			boost--;
	}

	public void weightPatchDown() {
		if (weight > -14)
			weight--;
	}

	public void glidePatchDown() {
		if (glide > -14)
			glide--;
	}

	#endregion Negative

	#endregion Patch Stat Modifiers
}
