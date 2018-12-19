using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyWalk : MonoBehaviour {

	#region Variable Declaration

	public float moveSpeed;									
	private bool onStar, jumpingOnStar, isGrounded;
	private Rigidbody rb;
	private SphereCollider sphereCol;
	private Vector3 moveDirection;
	private int jumpPuffs, canJump;
	private Animator anim;
	private JetStarParticles jsp;
	private SwerveStarEffects sse;
	public GameObject cam;
	private UpdateUI ui;
	private int jumpCharge;
	private int spinTimer, spinTimer2;

	private AirRide ar;
	private GameObject airRide;

	private Vector3 endPos;

	#endregion VariableDeclaration

	// Use this for initialization
	void Start () {
		jumpPuffs = 0;
		canJump = 0;
		rb = GetComponent<Rigidbody>();
		//distToGround = (GetComponent<Collider>().bounds.extents.y / 2);
		jumpingOnStar = false;
		anim = GetComponent<Animator> ();
		sphereCol = GetComponent<SphereCollider> ();
		ui = GetComponent<UpdateUI> ();

	}

	// Update is called once per frame
	void Update () {
		if (!jumpingOnStar)
			GetInput();

	}

	void GetInput() {

		#region Kirby Walking

		if (!onStar) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")) * moveSpeed * Time.deltaTime;

			if (isGrounded) {
				jumpPuffs = 0;
			} 
				 
			// This is where the Kirby jump stuff goes
				
			if (Input.GetButtonDown ("Jump") && jumpPuffs < 5 && canJump == 0) {
				if (jumpPuffs == 0) {
					rb.velocity += Vector3.up * moveSpeed * 150 * Time.deltaTime;
					//canJump += 22;
				}
				else
					rb.velocity = Vector3.up * (moveSpeed / 2);
				anim.SetBool ("Jumping", true);

				jumpPuffs++;
				canJump += 50;
			}
				
			// Limits jumps
			if (canJump > 0)
				canJump--;
			if (canJump > 23)
				anim.SetBool ("Jumping2", true);
			else
				anim.SetBool ("Jumping2", false);

			if (moveDirection != Vector3.zero) {
				rb.rotation = Quaternion.LookRotation (moveDirection);
				if (!anim.GetBool("Jumping"))
					anim.SetBool ("Running", true);
			} else {
				anim.SetBool ("Running", false);
			}




			rb.MovePosition (transform.position + moveDirection);
		}

		#endregion Kirby Walking

		#region Kirby on a Ride

		// While riding a star Input
		if (onStar && ar != null) {

			anim.SetBool ("Running", false);
			anim.SetBool ("Jumping", false);
			anim.SetBool ("Jumping2", false);

			if (ar.isGliding) {
				// The gravity aspect - modify to only work while the glide stat isn't dead.
				print("gliding!");
				rb.AddForce (Vector3.down * 100);
			}
			// On ground
			//moveDirection = new Vector3 (0,0,(Mathf.Lerp (moveDirection.y, ar.topSpeed ,Time.deltaTime * ar.acceleration))); // Need this to push from back of vehicle
			//rb.mass *= 2;
			if (ar.acceleration < 1) {
				ar.acceleration += ar.accelerationRate;
			}
			if (ar.acceleration > 1) {
				ar.acceleration -= (ar.accelerationRate * 3);
				if (ar.acceleration < 1)
					ar.acceleration = 1;
			}
			// Charging and jumping off star
			if (Input.GetButton ("Jump")) {
				anim.SetBool ("Charging", true);
				if (ar.charge < 100)
					ar.charge += ar.chargeAmt;
				if (Input.GetKey(KeyCode.S)) {
					jumpCharge++;
					if (jumpCharge > 200)
						StartCoroutine(JumpOffStar ());
				}
				if (ar != null)
					ar.acceleration -= 3 * ar.accelerationRate;
			}
			if (Input.GetButtonUp ("Jump")) {
				if (ar.charge >= 100)
					ar.acceleration = 1 + (ar.boost * 0.125f);
				ar.charge = 0;
				jumpCharge = 0;
				anim.SetBool ("Charging", false);

			}
				
			// Turning
			if (ar != null) {
				Quaternion q = Quaternion.AngleAxis ((Input.GetAxis ("Horizontal") * (ar.turn / 2)), transform.up) * transform.rotation;
				if (q != transform.rotation && ar.acceleration > 0.65f) {
					ar.acceleration -= 1.5f * ar.accelerationRate;
				}
				transform.rotation = q;
			}
			if (Input.GetKey(KeyCode.D)) {
				anim.SetBool ("TurningRight", true);
				anim.SetBool ("TurningLeft", false);
			}
			if (Input.GetKey(KeyCode.A)) {
				anim.SetBool ("TurningLeft", true);
				anim.SetBool ("TurningRight", false);
			}
			if (!Input.GetKey(KeyCode.A) && (!Input.GetKey(KeyCode.D))) {
				anim.SetBool ("TurningLeft", false);
				anim.SetBool ("TurningRight", false);
			}

			// Movement
			if (ar != null) {
				if ((ar.topSpeed * ar.acceleration) <= 0) {
					//ar.topSpeed = 0;
					ar.acceleration = 0;
				} else
					rb.AddRelativeForce (Vector3.forward * 100);	
				rb.velocity = Vector3.ClampMagnitude (rb.velocity, ar.topSpeed * ar.acceleration);
			}
			//print(rb.velocity);

			// Spin attack
			// Activated by hitting left and right repeatedly in a short time.
			if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) 
				spinTimer++;
			
			if (spinTimer > 10 && spinTimer < 100) {
				if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) {
					spinTimer2++;
				}
			}

			if (spinTimer2 > 10 && spinTimer2 < 100) {
				if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) {
					spinTimer = 0;
					spinTimer2 = 0;
					StartCoroutine (spinAttack ());
				}
			}

			if (spinTimer > 150)
				spinTimer = 0;
			if (spinTimer2 > 150)
				spinTimer2 = 0;

			#endregion Kirby on a Ride
			
		}

	}

	// Jumping off of the Air Ride
	private IEnumerator JumpOffStar() {
		// Kirby no longer dependent on vehicle, and vice versa.
		airRide.transform.parent = null;
		airRide.gameObject.tag = "AirMachine";
		ar.charge = 0;
		ar.resetRotation ();
		ar = null;
		onStar = false;
		jumpCharge = 0;


		cam.transform.parent = null;
		cam.GetComponent<CameraControl> ().enabled = true;


		// Make all vehicle stats go back

		// Resetting particle effects for vehicles.
		if (jsp != null) {
			jsp.StopParticles ();
			jsp = null;
		}
		if (sse != null) {
			sse.StopParticles ();
			sse = null;
		}


		// Undo UI for vehicles
		ui.enabled = false;

		// Jumps Kirby Up, for a "Pop off" effect.
		rb.velocity += Vector3.up * moveSpeed * Time.deltaTime;

		// He isn't charging anymore, so appropriate the animations.
		anim.SetBool ("Charging", false);
		anim.SetBool ("JumpingOff", true);
		anim.SetBool ("Jumping", false);
		anim.SetBool ("Jumping2", false);
		anim.SetBool ("Riding", false);

		// Don't re-enable his sphere collider for a few seconds so he won't instantly hop back on a ride.
		yield return new WaitForSeconds(1f);
		sphereCol.enabled = true;
		anim.SetBool ("JumpingOff", false);
	}	

	// A script that moves Kirby to the "seat" of the ride.
	IEnumerator JumpOnStar(Transform mover, Vector3 destination, float speed) {
		// This looks unsafe, but Unity uses an epsilon when comparing vectors.
		while(mover.position != destination) {
			mover.position = Vector3.MoveTowards(
				mover.position,
				destination,
				speed * Time.deltaTime);
				rb.rotation = Quaternion.Euler(0,0,0);
				transform.rotation = Quaternion.Euler(0,0,0);
			// Wait a frame and move again.
			yield return null;
		}
	}


	// Detects Trigger Collision
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "AirMachine" && onStar == false) {
			//print ("Air Ride found!");

			anim.SetBool ("Hopping on", true);

			//Certain Rides have special features
			if (col.gameObject.name == "JetStar")
				jsp = col.gameObject.GetComponent<JetStarParticles> ();
			if (col.gameObject.name == "SwerveStar")
				sse = col.gameObject.GetComponent<SwerveStarEffects> ();


			endPos = col.gameObject.GetComponent<AirRide> ().getSitHeight ();
			jumpingOnStar = true;
			col.gameObject.tag = "KirbyMachine";
			StartCoroutine(JumpOnStar(this.transform, endPos, moveSpeed * Time.deltaTime * 20));
			StartCoroutine (RiderOn (col.gameObject));
			cam.transform.parent = transform;
			cam.GetComponent<CameraControl> ().enabled = false;
			// Kirby no more hit detection
			sphereCol.enabled = false;
			rb.isKinematic = true;
		}
	}

	// Edits the variables to assign Kirby's ride.
	IEnumerator RiderOn(GameObject g) {
		yield return new WaitForSeconds (0.6f);
		g.transform.SetParent(this.transform);
		jumpingOnStar = false;
		onStar = true;
		anim.SetBool ("Riding", true);
		anim.SetBool ("Hopping on", false);
		rb.isKinematic = false;
		if (jsp != null)
			jsp.StartParticles ();
		if (sse != null)
			sse.StartParticles ();

		ar = g.GetComponent<AirRide> ();
		ui.enabled = true;
		airRide = g.gameObject;

	}

	#region OnFloorOrNot

	void OnCollisionEnter(Collision theCollision)
	{
		if (theCollision.gameObject.tag == "Floor" || theCollision.gameObject.tag == "Box")
		{
			isGrounded = true;
			anim.SetBool ("Jumping", false);
		}
	}

	void OnCollision(Collision theCollision)
	{
		if (theCollision.gameObject.tag == "Floor" || theCollision.gameObject.tag == "Box")
		{
			isGrounded = true;
			anim.SetBool ("Jumping", false);
		}
	}

	//consider when character is jumping .. it will exit collision.
	void OnCollisionExit(Collision theCollision)
	{
		if (theCollision.gameObject.tag == "Floor" || theCollision.gameObject.tag == "Box")
		{
			isGrounded = false;
		}
	}

	#endregion OnFloorOrNot

	// Spin attack code
	private IEnumerator spinAttack() 
	{
		
		float timesToRotate = 10;     //How many times to rotate? 10 is normal.
		//Quaternion g = Quaternion.AngleAxis ((Input.GetAxis ("Horizontal") * (ar.turn / 2)), transform.up) * transform.rotation;
		timesToRotate *= 18;
		while(timesToRotate > 0)     //While the time is more than zero...
		{
			Vector3 rotation = new Vector3(0, 0, 20);
			airRide.transform.Rotate(rotation);     //...rotate the object.
			//transform.Rotate (rotation);
			timesToRotate--;
			anim.SetBool ("Spinning", true);


			yield return null;     //Loop the method.
		}
			 
		anim.SetBool ("Spinning", false);
		//airRide.transform.rotation = q * Quaternion.AngleAxis(0, Vector3.forward);


	}

	// Basic getters and setters.

	public bool isRider() {
		return onStar;
	}

	public int getJumpPuffs() {
		return jumpPuffs;
	}

	public AirRide getAirRide() {
		return ar;
	}

	public GameObject getAirRideGameObject() {
		return airRide;
	}

	public bool getIsGrounded() {
		return isGrounded;
	}

	public bool getOnStar() {
		return onStar;
	}

	public int getCanJump() {
		return canJump;
	}

}
