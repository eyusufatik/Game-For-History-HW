using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	private Question[] questions;
	private int currentQuestionNumber;
	private int correctAnswers;

	public Text questionText;
	public Text buttonA;
	public Text buttonB;
	public Text buttonC;
	public Text pointsText;
	public Text GOpointsText;
	public Text GOhighScoreText;

	public GameObject mainGroup;
	public GameObject gameOverGroup;
	public GameObject scrollbar;

	void Start () {
		correctAnswers = 0; 
		currentQuestionNumber = 0;
		ReadQuestions();
		// 5 choices don't leave enough room for long answers
		// so reduce the number of answers to 3
		// if the correct answer is E or D cahange it to A,B or C randomly
		ChangeTo3Choices(questions);
		RandomizeOrder(questions);
		DisplayNextQuestion();
	}

	// Reads json file and initializes questions array
	private void ReadQuestions(){
		string filePath = Application.streamingAssetsPath + "/sorular.json";
		string jsonData = "";
		if (Application.platform == RuntimePlatform.Android)
		{
			WWW reader = new WWW(filePath);
			while (!reader.isDone) { }
			jsonData = reader.text;
		}
		else
		{
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
		scrollbar.GetComponent<Scrollbar>().value = 1;
		Question question = questions[currentQuestionNumber];
		questionText.text = question.question;
		buttonA.text = "A) " + question.A;
		buttonB.text = "B) " + question.B;
		buttonC.text = "C) " + question.C;

		Debug.Log(question.answer);
	}

	private void CorrectAnswer(){
		Debug.Log("correct");
		correctAnswers++;
		pointsText.text = correctAnswers.ToString();

		currentQuestionNumber++;
		DisplayNextQuestion();
	}

	private void WrongAnswer(){
		Question question = questions[currentQuestionNumber];
		if(question.answer.CompareTo("A")==0){
			buttonA.GetComponentInParent<Image>().color = new Color32(0,255,31,255);
		}
		else if(question.answer.CompareTo("B")==0){
			buttonB.GetComponentInParent<Image>().color = new Color32(0,255,31,255);
		}
		else if(question.answer.CompareTo("C")==0){
			buttonC.GetComponentInParent<Image>().color = new Color32(0,255,31,255);
		}
		Debug.Log("wrong");
		GameOver();
	}

	public void TimesUp(){
		Debug.Log("annen");
		GameOver();
	}

	public void GameOver(){
		GetComponent<HighScore>().CheckAndRecordHighScore(correctAnswers);
		gameOverGroup.SetActive(true);
		GOpointsText.text = "Doğru Sayısı:\n" + correctAnswers.ToString();
		int highScore = GetComponent<HighScore>().GetHighScore();
		GOhighScoreText.text = "Yüksek Skor:\n" + highScore.ToString();
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

	public void Replay_Click(){
		SceneManager.LoadScene("GameScene");
	}
}
