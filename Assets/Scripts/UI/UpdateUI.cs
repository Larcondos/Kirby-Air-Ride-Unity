using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {

	// General

	private AirRide ar;
	private GameObject player;
	public Image kirbySpeedometer, speedometer;
	public GameObject speedometerNums, HPBar;

	// Charge Bar

	public Image topChargeBar, botChargeBar;

	private Gradient rainbow;
	private GradientColorKey[] colorKey;
	private GradientAlphaKey[] alphaKey;

	private bool flashing, startedFlash;

	// Speed Numbers

	public Text speed1, speed2;
	private float speed;
	private float updateDelay = 0.05f, updateDelayStart = 0f;

	// HP Bar

	public Image KirbyHead, maxHPBar, curHPBar, curHPBarBorder;



	// Use this for initialization
	void Start () {	
		initGradient ();
		player = GameObject.FindGameObjectWithTag ("Kirby");

		topChargeBar.fillAmount = 0;
		botChargeBar.fillAmount = 0;
		/*kirbySpeedometer.enabled = false;
		speedometer.enabled = true;
		speedometerNums.SetActive (true);
		HPBar.SetActive (true);
		*/
	}
	
	// Update is called once per frame
	void Update () {

		#region General
		if (ar == null)
			ar = player.GetComponent<KirbyWalk> ().getAirRide ();

		#endregion General

		#region ChargeBar

		// Sets the fill and color of charge bar based on the amount that is full. I like this code.
		if (ar.charge < 100) {
			topChargeBar.fillAmount = (ar.charge / 100);
			topChargeBar.color = rainbow.Evaluate (ar.charge / 100);

			botChargeBar.fillAmount = (ar.charge / 100);
			botChargeBar.color = rainbow.Evaluate (ar.charge / 100);
		}

		if (ar.charge == 100 && !flashing) {
			flashing = true;
		}

		if (ar.charge == 0) {
			flashing = false;
			startedFlash = false;
		}

		if (flashing && !startedFlash) {
			StartCoroutine (Flash ());
			startedFlash = true;
		}

		#endregion ChargeBar

		#region SpeedText
		if (Time.time > updateDelayStart) {
			speed = ar.acceleration * ar.topSpeed;
			//print ("I think speed is: " + speed);
			speed1.text = Mathf.Floor(speed).ToString().PadLeft(3, '0');
			//print (speed.ToString("000"));
			float decimalPart = speed - Mathf.Floor(speed);
			speed2.text = decimalPart.ToString("F2").Substring(1);;
			updateDelayStart += updateDelay;
		}


		#endregion SpeedText

		#region HP

		//maxHPBar.rectTransform.sizeDelta = new Vector2 (60, ar.maxHP + 20);




		#endregion HP
	}

	IEnumerator Flash() {
		while (flashing) {
			topChargeBar.color = Color.white;
			botChargeBar.color = Color.white;
			yield return new WaitForSeconds (0.15f);
			topChargeBar.color = Color.red;
			botChargeBar.color = Color.red;
			yield return new WaitForSeconds (0.15f);
		}
	}


	// Initializes a gradient that is rainbow.
	void initGradient() {
		rainbow = new Gradient ();
		colorKey = new GradientColorKey[4];
		colorKey [0].color = Color.blue;
		colorKey [0].time = 0.0f;
		colorKey [1].color = Color.green;
		colorKey [1].time = 0.33f;
		colorKey [2].color = Color.yellow;
		colorKey [2].time = 0.66f;
		colorKey [3].color = Color.red;
		colorKey [3].time = 1.0f;

		alphaKey = new GradientAlphaKey[4];
		alphaKey[0].alpha = 1.0f;
		alphaKey[0].time = 0.0f;
		alphaKey[1].alpha = 1.0f;
		alphaKey[1].time = 0.33f;
		alphaKey[2].alpha = 1.0f;
		alphaKey[2].time = 0.66f;
		alphaKey[3].alpha = 1.0f;
		alphaKey[3].time = 1.0f;

		rainbow.SetKeys (colorKey, alphaKey);
	}

	// When this code is disabled, switch back to the other UI.
	void OnDisable()
	{
		ar = null;
		topChargeBar.fillAmount = 0;
		botChargeBar.fillAmount = 0;
		flashing = false;
		kirbySpeedometer.enabled = true;
		speedometer.enabled = false;
		speedometerNums.SetActive (false);
		HPBar.SetActive (false);
	}

	// When enabled, turn on this UI and turn off the other UI.
	void OnEnable()
	{
		//ar = player.GetComponent<KirbyWalk> ().getAirRide ();
		kirbySpeedometer.enabled = false;
		speedometer.enabled = true;
		speedometerNums.SetActive (true);
		HPBar.SetActive (true);
	}
		
}
