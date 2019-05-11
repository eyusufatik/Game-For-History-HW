using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
	public float seconds;
	public Text CountdownLabel;
	// Use this for initialization
	void Start () {
		CountdownLabel.text = ConvertToTextFormat(seconds);
	}
	
	// Update is called once per frame
	void Update () {
		seconds -= Time.deltaTime;
		if(seconds >= 0){
			CountdownLabel.text = ConvertToTextFormat(seconds);
		}
		else{
			CountdownLabel.text = "0:00";
			GetComponent<Game>().TimesUp();
		}
	}

	string ConvertToTextFormat(float remainingSeconds){
		int minutes = (int) Mathf.Floor(remainingSeconds / 60f);
		int seconds = (int) Mathf.Floor(remainingSeconds - minutes * 60);
		return string.Format("{0}:{1:D2}",minutes,seconds);
	}
}
