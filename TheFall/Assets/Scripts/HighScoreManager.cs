using UnityEngine;
using UnityEngine.UI;
using System;

public class HighScoreManager : MonoBehaviour {
    public GameObject scoreRecord;
    public Transform scoreTable;
    public Color bgColor;

    public GameObject namePanel;
    public Text scoreText;

    HighScore currentScore;
    HighScores scoreList;

    int newScore = -1; //no new score
    string playerName;

    public OnlineHighScore dreamlo;

    //Flow 1 - Start() -> CheckLastScore() -> UpdateUI()
    //Flow 2 - Start() -> CheckLastScore() -> UpdateScoreTable() -> Ask for Name [Other Script] -> GetName() -> UpdateUI() 

    void Start() {
        //Disable score Table
        scoreTable.gameObject.SetActive(false);

        //Load Scores from file
        LoadScores();
        
        //Compare last score with high scores
        CheckLastScore();
    }

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

        //If score table empty
        if (scoreList.scores.Count == 0) {
            newScore = 0;
        }
        else {
            //Check if latest score is higher than existing scores
            for (int i = 0; i < scoreList.scores.Count; i++) {
                if (currentScore.score > scoreList.scores[i].score) {
                    //set to index in score table
                    newScore = i;
                    break;
                }
            }
        }

        //If not higher than existing scores
        if (newScore < 0) {
            //Check to see if less than 10 in table
            if (scoreList.scores.Count < 10) {
                newScore = scoreList.scores.Count;
                UpdateScoreTable(newScore);
            }
            else {
                UpdateUI();
            }
        }
        //else, update the score table with the new score
        else {
            UpdateScoreTable(newScore);
        }
    }

    void UpdateScoreTable(int place) {
        //insert new score
        scoreList.scores.Insert(newScore, currentScore);

        if (scoreList.scores.Count > 10) {
            //remove last score
            scoreList.scores.RemoveAt(scoreList.scores.Count - 1);
        }

        //Ask for Player Name
        scoreText.text = currentScore.score.ToString("N0");

        if (PlayerPrefs.HasKey("PlayerName")){
            playerName = PlayerPrefs.GetString("PlayerName");
            SaveOnlineScore();
            UpdateUI();
        }
        else {
            namePanel.SetActive(true);
        }
    }

    public void SaveOnlineScore() {
        string platform = "";
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            platform = "Web";
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer) {
            platform = "Windows";
        }
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            platform = "Windows";
        }
        dreamlo.AddScore(playerName, currentScore.score, 0, platform);
    }

    //Called after receiving name
    public void GetName(string name) {
        PlayerPrefs.SetString("PlayerName", name);
        playerName = name;
        UpdateUI();
    }

    void UpdateUI() {
        for (int i = 0; i < 10; i++) {
            GameObject record = Instantiate(scoreRecord, Vector3.zero, Quaternion.identity) as GameObject;
            record.transform.SetParent(scoreTable, false);
            ScoreUI ui = record.GetComponent<ScoreUI>();

            //Change row for new (current) high score
            if (i == newScore) {
                ui.background.color = bgColor;
                scoreList.scores[i].name = playerName;
            }

            //Sets name, score, date to UI
            if (scoreList.scores != null && scoreList.scores.Count > (i)) {
                ui.SetUI(scoreList.scores[i].name, scoreList.scores[i].score, scoreList.scores[i].date, i + 1);
            }
            //Fills "-" in when score table not full 
            else {
                ui.SetUI(i + 1);
            }
        }

        //Display Score Table
        scoreTable.gameObject.SetActive(true);

        //Save Score
        DataAccess.Save(scoreList, Data.ScoreTable);
    }
}
