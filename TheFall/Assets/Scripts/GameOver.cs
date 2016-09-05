using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

    public Text scoreText;
    public Text highScoreText;

    int score;
	int highScore;
	int mode;

	void Start(){
        //Modes
        //0 = Story
        //1 = Arcade
        //mode = (PlayerPrefs.HasKey("GameMode")) ? PlayerPrefs.GetInt("GameMode") : Modes.Arcade;
        mode = Modes.Arcade;
		if(mode == Modes.Arcade){
			score = (PlayerPrefs.HasKey("Score")) ?
					PlayerPrefs.GetInt("Score") :
					0;

			highScore = (PlayerPrefs.HasKey("HighScore")) ? 
						PlayerPrefs.GetInt("HighScore") :
						0;

            if (score == highScore) {
                scoreText.text = score.ToString("n0");
                highScoreText.text = "New High Score!";
            }
            else {
                scoreText.text = "Your Score: " + score.ToString("n0");
                highScoreText.text = "High Score: " + highScore.ToString("n0");
            }
        }
    }
}