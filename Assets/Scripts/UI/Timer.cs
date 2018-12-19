using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	// This entire script is used to CountDown from 5 minutes. Mostly formatting.

	public Text TimerText;
	public float time = 60 ;
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		int intTime = (int)time;
		int minutes = intTime / 60;
		int seconds = intTime % 60;
		float fraction = time * 100;
		fraction = (fraction % 100);
		string timeText = string.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
		TimerText.text = timeText;

	}
}
