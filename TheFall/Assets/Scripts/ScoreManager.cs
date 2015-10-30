using UnityEngine;
using System.Collections;

public class ScoreManager : Singleton<ScoreManager> {

	int score;
	int highScore;
	int level;

	void OnEnable(){
		EventManager.StartListening(Events.BossHit, UpdateLevel);
	}

	void OnDisable(){
		EventManager.StopListening(Events.BossHit, UpdateLevel);
	}

	void Awake () {
		score = 0;
		level = 1;
	}

	//Level Functions
	public int GetLevel(){
		return level;
	}

	void UpdateLevel(){
		level += 1;
	}

	//Score Functions
	void CalculateScore(){
		score = Mathf.RoundToInt(Time.timeSinceLevelLoad) * 100 * level;
	}

	public int GetScore(){
		CalculateScore();
		return score;
    }

	void SetScore(){
		CalculateScore();
		PlayerPrefs.SetInt("Score", score);
	}

    void SetHighScore(){
    	CalculateScore();
    	if(PlayerPrefs.HasKey("HighScore")){
			if(PlayerPrefs.GetInt("HighScore") < score) {
				PlayerPrefs.SetInt("HighScore",score);
			}
    	}
    	else{
			PlayerPrefs.SetInt("HighScore",score);
		}
	}

    public void SaveScores(){
    	SetScore();
    	SetHighScore();
    }
}