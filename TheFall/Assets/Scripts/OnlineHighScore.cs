using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class OnlineHighScore : MonoBehaviour {

    public GameObject scoreRecord;
    public Transform HighScorePanel;
    Dreamlo server;

    List<DreamloScore> scores;

    string url = "http://dreamlo.com/lb/57ff1b868af6031578e790f5/pipe/1";

    void Start() {
        server = GetComponent<Dreamlo>();
        scores = new List<DreamloScore>();
    }

    public IEnumerator CheckServer(GameObject go) {
        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error)) {
            //Error
            go.SetActive(false);
        }
        else {
            go.SetActive(true);
        }
    }

    //Save Score
    //Return new score table
    public IEnumerator SaveScore(string playerName, int score) {
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

        CoroutineWithData cd = new CoroutineWithData(this, server.AddScoreWithPipe(playerName, score, 1, platform));
        yield return cd.coroutine;

        cd = new CoroutineWithData(this, server.GetScores(100));
        yield return cd.coroutine;

        DisplayScores(cd.result.ToString());
    }

    void DisplayScores(string highscore) {
        scores = server.ToListHighToLow(highscore);

        for (int i = 0; i < scores.Count; i++) {
            GameObject go = Instantiate(scoreRecord);
            go.GetComponent<DreamloScoreRecord>().SetUI(scores[i], i + 1);
            go.transform.SetParent(HighScorePanel, false);
        }
    }
}