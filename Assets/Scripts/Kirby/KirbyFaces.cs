using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyFaces : MonoBehaviour {

	// These are 4 basic components of Kirby, his main body, mesh, animator, and the walking script in that order.
	public GameObject sphere;
	private SkinnedMeshRenderer mesh;
	private Animator anim;
	private KirbyWalk script;

	// This keeps track of whether or not he is "puffed", or inflated while jumping.
	private bool puffed = false;

	// This keeps the correct "puffed" face with consistent to how long he has been jumping.
	private int secs = 0;

	// The array of materials used for his faces. His angry face is not currently implemented.
	public Material[] mats;
	// mats[0] = normal face
	// mats[1] = puffed face
	// mats[2] = angry face
	// mats[3] = happy face
	// mats[4] = :o face

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		mesh = sphere.GetComponent<SkinnedMeshRenderer> ();
		script = GetComponent<KirbyWalk> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Check for running first, as it can be overlapped by more important faces.
		if (anim.GetBool ("Running"))
			mesh.material = mats [4];

		// Check for jumping next, as it overtakes the running face.
		if (anim.GetBool ("Jumping")) {
			mesh.material = mats [3];
			secs++;

			// If he has been in the air for 48 (arbitrary) frames and he is jumping again, he becomes "puffed".
			if (anim.GetBool ("Jumping2") && (secs > 48))
				puffed = true;
		}

		// If he isn't jumping, then reset his face back to the standard face, and he must be on the ground so we
		// can reset his "puffed" and "secs" variables to their standard.
		if (!anim.GetBool("Jumping")) {
			mesh.material = mats [0];
			puffed = false;
			secs = 0;
		}

		// If he is running, and not standard, give him his :o face
		if (anim.GetBool ("Running") && mesh.material != mats[0])
			mesh.material = mats [4];

		// If he is puffed then he needs the appropriate face
		if (puffed)
			mesh.material = mats [1];

		// If he has used all of his Jump Puffs, he is blowing out air with :o face
		if (script.getJumpPuffs () == 5)
			mesh.material = mats [4];
	}
}
