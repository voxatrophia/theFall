using UnityEngine;

public class HighScoreManager : MonoBehaviour {
    HighScore currentScore;
    HighScores scoreList;

    int newScore = -1; //no new score

    void Awake() {
        //currentScore = new HighScore();
        //scoreList = new HighScores();

        //Load Scores from file
        LoadScores();

        //Debug.Log("Deleting Local Scores");
        //DataAccess.Clear(Data.ScoreTable);
    }

    void LoadScores() {
        currentScore = (HighScore)DataAccess.Load(Data.LastScore);
        if (currentScore == null) {
            currentScore = new HighScore();
        }
        else {
            //delete last score
            DataAccess.Clear(Data.LastScore);
        }

        scoreList = (HighScores)DataAccess.Load(Data.ScoreTable);
        if (scoreList == null) {
            scoreList = new HighScores();
        }
    }

    //checks last score vs score table
    //Saves player name to score table
    //saves score table to file
    //returns score table
    public HighScores GetScoreTable(string playerName) {
        CheckLastScore();

        if (newScore >= 0) {
            scoreList.scores[newScore].name = playerName;
        }

        DataAccess.Save(scoreList, Data.ScoreTable);

        return scoreList;
    }

    public HighScore GetLastScore() {
        return currentScore;
    }

    public int GetPlace() {
        return newScore;
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
        }
        //else, update the score table with the new score
        else {
            UpdateScoreTable(newScore);
        }
    }

    void UpdateScoreTable(int place) {
        //insert new score
        scoreList.scores.Insert(place, currentScore);

        if (scoreList.scores.Count > 10) {
            //remove last score
            scoreList.scores.RemoveAt(scoreList.scores.Count - 1);
        }
    }

}
