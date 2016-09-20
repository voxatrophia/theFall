using System;
using UnityEngine;

public class SaveTest : MonoBehaviour {

    HighScore score;
    HighScore newScore;

    void Start() {
        score = new HighScore();
        score.name = "Text";
        score.score = 12345;
        score.date = DateTime.Today;
    }

    public void SaveData() {
        DataAccess.Save(score, "highscore");
    }

    public void Load() {
        newScore = (HighScore)DataAccess.Load("highscore");
        if (newScore != null) {
            Debug.Log(newScore.score);
        }
    }
}
