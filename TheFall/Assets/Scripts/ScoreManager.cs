using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager> {

	int score;
//	int highScore;
	//int multipler;
    public Text scoreText;


	//void OnEnable(){
	//	EventManager.StartListening(Events.BossHit, UpdateMultiplier);
 //   }

 //   void OnDisable(){
	//	EventManager.StopListening(Events.BossHit, UpdateMultiplier);
	//}

	void Awake () {
		score = 0;
        //multipler = 1;
//        highScore = GetHighScore();
	}

    void Start() {
        StartCoroutine(StartScore());
    }

    IEnumerator StartScore() {
        while (true) {
            score = Mathf.RoundToInt(Time.timeSinceLevelLoad) * 100;
            scoreText.text = "Score: " + score.ToString("n0");
            yield return Yielders.Get(1);
        }
    }

	////Level Functions
	//public int GetMultiplier(){
	//	return multipler;
	//}

	//void UpdateMultiplier(){
 //       multipler += 1;
 //       EventManager.TriggerEvent(Events.UpdateMultiplier);
	//}

 //   public int GetRawScore() {
 //       return score;
 //   }

	////Score Functions
	//void CalculateScore(){
	//	score = Mathf.RoundToInt(Time.timeSinceLevelLoad) * 100 * multipler;
	//}

	//public int GetScore(){
	//	CalculateScore();
	//	return score;
 //   }

 //   public int GetHighScore() {
 //       return (PlayerPrefs.HasKey("HighScore")) ? PlayerPrefs.GetInt("HighScore") : 0;
 //   }

    void SetScore(){
        score = Mathf.RoundToInt(Time.timeSinceLevelLoad) * 100;
        PlayerPrefs.SetInt("Score", score);
        HighScore highscore = new HighScore { name = "Player", score = score, date = DateTime.Today };
        DataAccess.Save(highscore, Data.LastScore);
	}

 //   void SetHighScore(){
 //   	if(PlayerPrefs.HasKey("HighScore")){
	//		if(PlayerPrefs.GetInt("HighScore") < score) {
	//			PlayerPrefs.SetInt("HighScore", score);
	//		}
 //   	}
 //   	else{
	//		PlayerPrefs.SetInt("HighScore", score);
	//	}
	//}

    public void SaveScores(){
    	SetScore();
    	//SetHighScore();
    }
}