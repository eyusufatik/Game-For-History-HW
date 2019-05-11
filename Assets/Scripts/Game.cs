using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
	private Question[] questions;
	private int currentQuestionNumber;

	public Text questionText;
	public Text buttonA;
	public Text buttonB;
	public Text buttonC;
	
	// Use this for initialization
	void Start () {
		ReadQuestions();
		// 5 choices don't leave enough room for long answers
		// so reduce the number of answers to 3
		// if the correct answer is E or D cahange it to A,B or C randomly
		ChangeTo3Choices(questions);
		RandomizeOrder(questions);
		DisplayNextQuestion();
	}
	
	void Update(){

	}

	// Reads json file and initializes questions array
	private void ReadQuestions(){
		string filePath = Path.Combine(Application.streamingAssetsPath, "sorular.json");
		string jsonData = "";
		if(File.Exists(filePath)){
			jsonData = File.ReadAllText(filePath);
		}
		questions = JsonHelper.FromJson<Question>(jsonData);
	}

	// Shuffles array
	private void RandomizeOrder(Question[] array){
		for(int i = 0; i < array.Length; i++){
			int j =  Random.Range(0,array.Length-1);
			Question temp = array[i];
			array[i] = array[j];
			array[j] = temp;
		}
	}

	// Changes questions from 5 choice format to 3 choice format 
	private void ChangeTo3Choices(Question[] quests){
		for (int i = 0; i < quests.Length; i++){
			Question tempQuestion = quests[i];
			if (tempQuestion.answer.CompareTo("E")==0){
				Debug.Log("it happened");
				switch(Random.Range(0,3)){
					case 0:
						tempQuestion.A = tempQuestion.E;
						quests[i].answer = "A";
						break;
					case 1:
						tempQuestion.B = tempQuestion.E;
						quests[i].answer = "B";
						break;
					case 2:
						tempQuestion.C = tempQuestion.E;
						quests[i].answer = "C";
						break;
				}
				quests[i] = tempQuestion;
			} 
			else if (tempQuestion.answer.CompareTo("D")==0){
				Debug.Log("it happened");
				switch(Random.Range(0,3)){
					case 0:
						tempQuestion.A = tempQuestion.D;
						tempQuestion.answer = "A";
						break;
					case 1:
						tempQuestion.B = tempQuestion.D;
						tempQuestion.answer = "B";
						break;
					case 2:
						tempQuestion.C = tempQuestion.D;
						tempQuestion.answer = "C";
						break;
				}
				quests[i] = tempQuestion;
			}
		}
	}

	private void DisplayNextQuestion(){
		Question question = questions[currentQuestionNumber];
		questionText.text = question.question;
		buttonA.text = "A) " + question.A;
		buttonB.text = "B) " + question.B;
		buttonC.text = "C) " + question.C;
	}

	private void CorrectAnswer(){
		Debug.Log("correct nigga");
		currentQuestionNumber++;
		DisplayNextQuestion();
	}

	private void WrongAnswer(){
		Debug.Log("wrong nigga");
	}

	public void ButtonA_Click(){
		if(questions[currentQuestionNumber].answer.CompareTo("A")==0){
			CorrectAnswer();
		}else{
			WrongAnswer();
		}
	}

	public void ButtonB_Click(){
		if(questions[currentQuestionNumber].answer.CompareTo("B")==0){
			CorrectAnswer();
		}else{
			WrongAnswer();
		}
	}

	public void ButtonC_Click(){
		if(questions[currentQuestionNumber].answer.CompareTo("C")==0){
			CorrectAnswer();
		}else{
			WrongAnswer();
		}
	}
}
