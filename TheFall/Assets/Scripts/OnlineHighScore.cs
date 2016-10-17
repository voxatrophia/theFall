using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public struct DreamloScore {
    public string name;
    public int score;
    public int seconds;
    public string shortText;
    public DateTime dateString;
}

public class OnlineHighScore : MonoBehaviour {

    public GameObject scoreText;
    public Transform panel;

    string dreamloURL = "http://dreamlo.com/lb/";

    public string privateCode = "";
    public string publicCode = "";

    List<DreamloScore> scores;

    //High scores will be returned as text
    string highScores = "";

    void Start() {
        scores = new List<DreamloScore>();
        LoadScores();
        //for (int i = 0; i < 100; i++) {
        //    GameObject go = Instantiate(scoreText);
        //    go.GetComponent<ScoreUI>().playerName.text = i.ToString();
        //    go.transform.SetParent(panel);
        //}
    }

    IEnumerator GetScores() {
        highScores = "";
        WWW www = new WWW(dreamloURL + publicCode + "/pipe/100");
        yield return www;
        highScores = www.text;

        scores = ToListHighToLow();
        DisplayScores();
    }

    void DisplayScores() {
        for (int i = 0; i < scores.Count; i++) {
            GameObject go = Instantiate(scoreText);
            go.GetComponent<DreamloScoreRecord>().SetUI(scores[i], i+1);
            go.transform.SetParent(panel,false);
        }
    }

    IEnumerator GetSingleScore(string playerName) {
        highScores = "";
        WWW www = new WWW(dreamloURL + publicCode + "/pipe-get/" + WWW.EscapeURL(playerName));
        yield return www;
        highScores = www.text;
    }

    public void LoadScores() {
        StartCoroutine(GetScores());
    }

    string Cleanse(string s) {
        s = s.Replace("+", " ");
        return s;
    }

    string Clean(string s) {
        s = s.Replace("/", "");
        s = s.Replace("|", "");
        return s;
    }




    public void AddScore(string playerName, int totalScore) {
        StartCoroutine(AddScoreWithPipe(playerName, totalScore));
    }

    public void AddScore(string playerName, int totalScore, int totalSeconds) {
        StartCoroutine(AddScoreWithPipe(playerName, totalScore, totalSeconds));
    }

    public void AddScore(string playerName, int totalScore, int totalSeconds, string shortText) {
        StartCoroutine(AddScoreWithPipe(playerName, totalScore, totalSeconds, shortText));
    }

    // This function saves a trip to the server. Adds the score and retrieves results in one trip.
    IEnumerator AddScoreWithPipe(string playerName, int totalScore) {
        playerName = Clean(playerName);

        WWW www = new WWW(dreamloURL + privateCode + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString());
        yield return www;
        highScores = www.text;
    }

    IEnumerator AddScoreWithPipe(string playerName, int totalScore, int totalSeconds) {
        playerName = Clean(playerName);

        WWW www = new WWW(dreamloURL + privateCode + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString() + "/" + totalSeconds.ToString());
        yield return www;
        highScores = www.text;
    }

    IEnumerator AddScoreWithPipe(string playerName, int totalScore, int totalSeconds, string shortText) {
        playerName = Clean(playerName);
        shortText = Clean(shortText);
        Debug.Log("called");
        WWW www = new WWW(dreamloURL + privateCode + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString() + "/" + totalSeconds.ToString() + "/" + shortText);
        yield return www;
        highScores = www.text;
    }

    //IEnumerator GetScores() {
    //    highScores = "";
    //    WWW www = new WWW(dreamloURL + publicCode + "/pipe");
    //    yield return www;
    //    highScores = www.text;
    //}

    //IEnumerator GetSingleScore(string playerName) {
    //    highScores = "";
    //    WWW www = new WWW(dreamloURL + publicCode + "/pipe-get/" + WWW.EscapeURL(playerName));
    //    yield return www;
    //    highScores = www.text;
    //}

    //public void LoadScores() {
    //    StartCoroutine(GetScores());
    //}


    public string[] ToStringArray() {
        if (this.highScores == null) return null;
        if (this.highScores == "") return null;

        string[] rows = this.highScores.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        return rows;
    }

    public List<DreamloScore> ToListLowToHigh() {
        DreamloScore[] scoreList = this.ToScoreArray();

        if (scoreList == null) return new List<DreamloScore>();

        List<DreamloScore> genericList = new List<DreamloScore>(scoreList);

        genericList.Sort((x, y) => x.score.CompareTo(y.score));

        return genericList;
    }

    public List<DreamloScore> ToListHighToLow() {
        DreamloScore[] scoreList = this.ToScoreArray();

        if (scoreList == null) return new List<DreamloScore>();

        List<DreamloScore> genericList = new List<DreamloScore>(scoreList);

        genericList.Sort((x, y) => y.score.CompareTo(x.score));

        return genericList;
    }

    public DreamloScore[] ToScoreArray() {
        string[] rows = ToStringArray();
        if (rows == null) return null;

        int rowcount = rows.Length;

        if (rowcount <= 0) return null;

        DreamloScore[] scoreList = new DreamloScore[rowcount];

        for (int i = 0; i < rowcount; i++) {
            string[] values = rows[i].Split(new char[] { '|' }, System.StringSplitOptions.None);

            DreamloScore current = new DreamloScore();
            current.name = values[0];
            current.score = 0;
            current.seconds = 0;
            current.shortText = "";
            //current.dateString = "";
            if (values.Length > 1) current.score = CheckInt(values[1]);
            if (values.Length > 2) current.seconds = CheckInt(values[2]);
            if (values.Length > 3) current.shortText = values[3];
            if (values.Length > 4) current.dateString = Convert.ToDateTime(values[4]);
            scoreList[i] = current;
        }

        return scoreList;
    }



    // Keep pipe and slash out of names

    //string Clean(string s) {
    //    s = s.Replace("/", "");
    //    s = s.Replace("|", "");
    //    return s;

    //}

    int CheckInt(string s) {
        int x = 0;

        int.TryParse(s, out x);
        return x;
    }

}