using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : Singleton<ScoreManager> {

	int score;
	int highScore;
	int multipler;

	void OnEnable(){
		EventManager.StartListening(Events.BossHit, UpdateMultiplier);
	}

	void OnDisable(){
		EventManager.StopListening(Events.BossHit, UpdateMultiplier);
	}

	void Awake () {
		score = 0;
        multipler = 1;
	}

	//Level Functions
	public int GetMultiplier(){
		return multipler;
	}

	void UpdateMultiplier(){
        multipler += 1;
        EventManager.TriggerEvent(Events.UpdateMultiplier);
	}

	//Score Functions
	void CalculateScore(){
		score = Mathf.RoundToInt(Time.timeSinceLevelLoad) * 100 * multipler;
	}

	public int GetScore(){
		CalculateScore();
		return score;
    }

    public int GetHighScore() {
        return (PlayerPrefs.HasKey("HighScore")) ? PlayerPrefs.GetInt("HighScore") : 0;
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