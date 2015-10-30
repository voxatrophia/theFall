using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

	public Text scoreText;

	int score;
	int highScore;
	int mode;

	public void Restart(){
		MainController.SwitchScene(Scenes.Main);
	}

	void Start(){
		//Modes
		//0 = Story
		//1 = Arcade
		mode = (PlayerPrefs.HasKey("GameMode")) ? PlayerPrefs.GetInt("GameMode") : Modes.Arcade;

		if(mode == Modes.Arcade){
			score = (PlayerPrefs.HasKey("Score")) ?
					PlayerPrefs.GetInt("Score") :
					0;

			highScore = (PlayerPrefs.HasKey("HighScore")) ? 
						PlayerPrefs.GetInt("HighScore") :
						0;

			scoreText.text = (score == highScore) ?
							"Your score: " + score.ToString("n0") + "\n" + "High Score: " + highScore.ToString("n0") + "\n" + "New High Score!" :
							"Your score: " + score.ToString("n0") + "\n" + "High Score: " + highScore.ToString("n0");
		}
	}
}