using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArcadeUI : MonoBehaviour {

	public Text levelText;
	public Text scoreText;

	int level;
	int score;

	void Start(){
		switch(LevelManager.Instance.GetMode()){
			case Modes.Story:
				gameObject.SetActive(false);
				break;
			case Modes.Arcade:
				UpdateLevel();
				score = 0;
				break;
		}
	}

	void Update(){
		score = ScoreManager.Instance.GetScore();
		scoreText.text = "Score: " + score;
	}

	void OnEnable(){
		EventManager.StartListening(Events.BossHit, UpdateLevel);
	}

	void OnDisable(){
		EventManager.StopListening(Events.BossHit, UpdateLevel);
	}

	public void UpdateLevel(){
		level = ScoreManager.Instance.GetLevel();
		levelText.text = "Level: " + level;
	}
}
