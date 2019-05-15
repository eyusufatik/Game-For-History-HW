using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {
	private int currentHighScore;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("high score")){
			currentHighScore = PlayerPrefs.GetInt("high score");
		}
		else{
			PlayerPrefs.SetInt("high score", 0);
		}
	}
	
	public void CheckAndRecordHighScore(int score){
		if(score > currentHighScore){
			currentHighScore = score;
			PlayerPrefs.SetInt("high score", score);
		}
	}

	public int GetHighScore(){
		return currentHighScore;
	}
}
