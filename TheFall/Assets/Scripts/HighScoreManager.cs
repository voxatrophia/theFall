using UnityEngine;
using System;

public class HighScoreManager : MonoBehaviour {
    public GameObject scoreRecord;
    public Transform scoreTable;

    HighScore currentScore;
    HighScores scoreList;

    int newScore = -1; //no new score

    void Start() {
        newScore = 2;
        LoadScores();
        CheckLastScore();
        UpdateUI();
    }

    //x at end of level, create highscore object and save to file
    //x start gameover scene, load last score and score table
    //x check if lastscore is in top scores
    //if so, add to score, change background color
    //allow input name - Need to use Modal window, doesn't work inline
    //remove last score
    //save scoretable

    void LoadScores() {
        currentScore = (HighScore)DataAccess.Load(Data.LastScore);
        if (currentScore == null) {
            currentScore = new HighScore();
        }

        scoreList = (HighScores)DataAccess.Load(Data.ScoreTable);
        if (scoreList == null) {
            scoreList = new HighScores();
        }
    }

    void CheckLastScore() {
        for(int i = 0; i < scoreList.scores.Count; i++) {
            if (currentScore.score > scoreList.scores[i].score) {
                Debug.Log("element in list: " + i);
                UpdateScoreTable(i);
                break;
            }
        }
    }

    void UpdateScoreTable(int place) {
        //insert new score after

    }

    void AskForName() {

    }

    void UpdateUI() {
        for (int i = 0; i < 10; i++) {
            GameObject record = Instantiate(scoreRecord, Vector3.zero, Quaternion.identity) as GameObject;

            if (scoreList.scores != null && scoreList.scores.Count > 0) {
                record.GetComponent<ScoreUI>().SetUI(scoreList.scores[i].name, scoreList.scores[i].score, scoreList.scores[i].date, i + 1);
            }
            else {
                record.GetComponent<ScoreUI>().SetUI(i + 1);
            }

            record.transform.SetParent(scoreTable, false);

            if (i == newScore) {
                record.GetComponent<ScoreUI>().nameField.gameObject.SetActive(true);
                Debug.Log(record.GetComponent<ScoreUI>().nameEntered);
            }
            else {
                record.GetComponent<ScoreUI>().nameField.gameObject.SetActive(false);
            }

        }
    }

}
