using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public struct DreamloScore {
    public string name;
    public int score;
    public int seconds;
    public string shortText;
    public DateTime dateString;
}

public class Dreamlo : MonoBehaviour {

    string dreamloURL = "http://dreamlo.com/lb/";

    [SerializeField]
    string privateCode;
    [SerializeField]
    string publicCode;

    //High scores will be returned as text
    string highScores = "";

    //IEnumerator GetScores() {
    //    highScores = "";
    //    WWW www = new WWW(dreamloURL + publicCode + "/pipe/");
    //    yield return www;
    //    highScores = www.text;
    //    //yield return highScores;
    //}

    public IEnumerator GetScores(int num) {
        highScores = "";
        WWW www = new WWW(dreamloURL + publicCode + "/pipe/" + num);
        yield return www;
        highScores = www.text;
        yield return highScores;
    }

    //IEnumerator GetSingleScore(string playerName) {
    //    highScores = "";
    //    WWW www = new WWW(dreamloURL + publicCode + "/pipe-get/" + WWW.EscapeURL(playerName));
    //    yield return www;
    //    highScores = www.text;
    //}

    //clearn for upload
    string Clean(string s) {
        s = s.Replace("/", "");
        s = s.Replace("|", "");
        return s;
    }

    //public void AddScore(string playerName, int totalScore) {
    //    StartCoroutine(AddScoreWithPipe(playerName, totalScore, -1, ""));
    //}

    //public void AddScore(string playerName, int totalScore, int totalSeconds) {
    //    StartCoroutine(AddScoreWithPipe(playerName, totalScore, totalSeconds, ""));
    //}

    //public void AddScore(string playerName, int totalScore, int totalSeconds, string shortText) {
    //    StartCoroutine(AddScoreWithPipe(playerName, totalScore, totalSeconds, shortText));
    //}

    public IEnumerator AddScoreWithPipe(string playerName, int totalScore, int totalSeconds, string shortText) {
        playerName = Clean(playerName);
        shortText = Clean(shortText);

        string url = dreamloURL + privateCode + "/add-pipe/" + WWW.EscapeURL(playerName) + "/" + totalScore.ToString() + "/";

        if (totalSeconds > 0) {
            url = url + totalSeconds.ToString() + "/";

            if (shortText != "") {
                url = url + WWW.EscapeURL(shortText);
            }
        }

        WWW www = new WWW(url);
        yield return www;

        highScores = www.text;

        yield return highScores;
    }

    public string[] ToStringArray(string highscores) {
        if (highscores == null) return null;
        if (highScores == "") return null;

        string[] rows = highScores.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        return rows;
    }

    public DreamloScore[] ToScoreArray(string highscores) {
        string[] rows = ToStringArray(highscores);
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

    public List<DreamloScore> ToListLowToHigh(string highscores) {
        DreamloScore[] scoreList = ToScoreArray(highscores);

        if (scoreList == null) return new List<DreamloScore>();

        List<DreamloScore> genericList = new List<DreamloScore>(scoreList);

        genericList.Sort((x, y) => x.score.CompareTo(y.score));

        return genericList;
    }

    public List<DreamloScore> ToListHighToLow(string highscores) {
        DreamloScore[] scoreList = ToScoreArray(highscores);

        if (scoreList == null) return new List<DreamloScore>();

        List<DreamloScore> genericList = new List<DreamloScore>(scoreList);

        genericList.Sort((x, y) => y.score.CompareTo(x.score));

        return genericList;
    }

    int CheckInt(string s) {
        int x = 0;

        int.TryParse(s, out x);
        return x;
    }

}
