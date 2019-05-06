using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Game : MonoBehaviour {
	private Question[] questions;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Reads json file and initializes questions array
	void ReadQuestions(){
		string filePath = Path.Combine(Application.streamingAssetsPath, "sorular.json");
		string jsonData = "";
		if(File.Exists(filePath)){
			jsonData = File.ReadAllText(filePath);
			Debug.Log("girdik");
		}
		questions = JsonHelper.FromJson<Question>(jsonData);
		Debug.Log(questions.Length);
	}
}
