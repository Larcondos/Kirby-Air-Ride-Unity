using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KirbySounds : MonoBehaviour {

	// Audio clips I can use.
	public AudioClip[] ac;

	// The source for audio to output.
	private AudioSource audio;

	// Is this Kirby's first jump?
	private bool firstJump;

	// Is this Kirby's last Jump?
	private int lastJumps;

	// Accesses Kirby's walking script.
	private KirbyWalk kw;

	// Accesses Kirby's animator.
	private Animator anim;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		kw = GetComponent<KirbyWalk> ();

		//Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () {
		// First jump sound effect.
		if (anim.GetBool("Jumping") && !firstJump) {
			firstJump = true;
			audio.PlayOneShot (ac[0]);
		}

		if (kw.getIsGrounded ()) {
			firstJump = false;
			lastJumps = 0;
		}


		// Sound effects to play based on amount of times jumped.
		if (!kw.getOnStar()) {

			if (Input.GetKeyDown (KeyCode.Space) && (kw.getJumpPuffs () <= 2))
				audio.PlayOneShot (ac [1]);
			if (Input.GetKeyDown (KeyCode.Space) && (kw.getJumpPuffs () > 2 && lastJumps < 3)) {
				audio.PlayOneShot (ac [2]);
				lastJumps++;
			}
		}


		// I threw this in here for debugging, I can press ESC in the built game to reload the scene.
		if (Input.GetKey(KeyCode.Escape))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}
}
