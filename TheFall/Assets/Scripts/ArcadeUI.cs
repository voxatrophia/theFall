using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArcadeUI : MonoBehaviour {

	public Text levelText;
	public Text scoreText;
    public Text multiplierText;
    public Text highScoreText;

	int level;
	int score;
    int multiplier;
    int highScore;

	void Start(){
		switch(LevelManager.Instance.GetMode()){
			case Modes.Story:
				gameObject.SetActive(false);
				break;
			case Modes.Arcade:
                //UpdateMultiplier();
				score = 0;
				break;
		}
        highScore = ScoreManager.Instance.GetHighScore();
        highScoreText.text = "High Score: " + highScore.ToString("n0");
    }

    void OnEnable() {
        EventManager.StartListening(Events.UpdateMultiplier, UpdateMultiplier);
    }

    void OnDisable() {
        EventManager.StopListening(Events.UpdateMultiplier, UpdateMultiplier);
    }


    void Update(){
		score = ScoreManager.Instance.GetScore();
		scoreText.text = "Score: " + score.ToString("n0");
        if (score > highScore) {
            UpdateHighScore();
        }
	}


    void UpdateHighScore() {
        highScoreText.text = "New High Score!";
    }

    public void UpdateMultiplier() {
        multiplier = ScoreManager.Instance.GetMultiplier();
        multiplierText.text = "x" + multiplier;
        multiplierText.canvasRenderer.SetAlpha(1.0f);
        multiplierText.CrossFadeAlpha(0f, 3f, true);
    }

	//public void UpdateLevel(){
	//	level = ScoreManager.Instance.GetLevel();
	//	levelText.text = "Multipler: " + level;
	//}
}
