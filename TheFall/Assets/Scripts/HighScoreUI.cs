using UnityEngine;
using UnityEngine.UI;
using System;

public class HighScoreUI : MonoBehaviour {
    public GameObject scoreRecord;
    public Transform scoreTable;
    public Color highlightColor;

    public GameObject namePanel;
    public Text scoreText;

    public GameObject LeaderboardButton;
    public GameObject LocalLeaderboardButton;

    HighScore currentScore;
    HighScores scoreList;

    OnlineHighScore online;

    HighScoreManager hs;

    bool localScore;

    void Awake() {
        //Local High Score not available in WebGL
        localScore = (Application.platform != RuntimePlatform.WebGLPlayer);
    }

    void Start() {
        hs = GetComponent<HighScoreManager>();
        if (hs == null) { Debug.Log("HighScoreManager not attached"); }

        online = GetComponent<OnlineHighScore>();
        if (online == null) { Debug.Log("OnlineHighScore not attached"); }

        //Disable Leaderboard button
        LeaderboardButton.SetActive(false);
        //Enable button if server is available
        StartCoroutine(online.CheckServer(LeaderboardButton));

        //Disable score Table
        scoreTable.gameObject.SetActive(false);

        //Get Last High Score
        currentScore = hs.GetLastScore();

        //Get Player Name
        GetName();
    }

    //Opens Enter Name panel
    void GetName() {
        scoreText.text = currentScore.score.ToString("N0");
        namePanel.SetActive(true);
    }

    //Called from Enter Name panel
    public void SetName(string playerName) {
        //Update scoretable data
        scoreList = hs.GetScoreTable(playerName);

        //Save score online
        StartCoroutine(online.SaveScore(playerName, currentScore.score));

        UpdateUI();
    }

    void UpdateUI() {
        int newScore = hs.GetPlace();

        for (int i = 0; i < 10; i++) {
            GameObject record = Instantiate(scoreRecord, Vector3.zero, Quaternion.identity) as GameObject;
            record.transform.SetParent(scoreTable, false);
            ScoreUI ui = record.GetComponent<ScoreUI>();

            //Change row for new (current) high score
            if (i == newScore) {
                ui.background.color = highlightColor;
            }

            //Sets name, score, date to UI
            if (scoreList.scores != null && scoreList.scores.Count > i) {
                ui.SetUI(scoreList.scores[i].name, scoreList.scores[i].score, scoreList.scores[i].date, i + 1);
            }
            //Fills "-" in when score table not full 
            else {
                ui.SetUI(i + 1);
            }
        }

        //Display Score Table
        scoreTable.gameObject.SetActive(true);
    }
}