using UnityEngine;

public class HighScoreManager : MonoBehaviour {
    HighScore currentScore;
    HighScores scoreList;

    int newScoreRank = -1; //no new score

    void Awake() {
        //Load Scores from file
        LoadScores();
    }

    void LoadScores() {
        currentScore = GetLastScore();

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

        if (newScoreRank >= 0) {
            scoreList.scores[newScoreRank].name = playerName;
        }

        DataAccess.Save(scoreList, Data.ScoreTable);

        return scoreList;
    }

    public HighScore GetLastScore() {
        //If current score is not set
        if (currentScore == null) {
            //pull current score from file
            HighScore score = (HighScore)DataAccess.Load(Data.LastScore);
            //if still null
            if (score == null) {
                //create empty score
                return new HighScore();
            }
            //current score pulled from file
            else {
                //delete last score
                DataAccess.Clear(Data.LastScore);
            }
        }

        return currentScore;
    }

    public int GetPlace() {
        return newScoreRank;
    }

    void CheckLastScore() {
        //If score table empty
        if (scoreList.scores.Count == 0) {
            newScoreRank = 0;
        }
        else {
            //Check if latest score is higher than existing scores
            for (int i = 0; i < scoreList.scores.Count; i++) {
                if (currentScore.score > scoreList.scores[i].score) {
                    //set to index in score table
                    newScoreRank = i;
                    break;
                }
            }
        }

        //If not higher than existing scores
        if (newScoreRank < 0) {
            //Check to see if less than 10 in table
            if (scoreList.scores.Count < 10) {
                newScoreRank = scoreList.scores.Count;
                UpdateScoreTable(newScoreRank);
            }
        }
        //else, update the score table with the new score
        else {
            UpdateScoreTable(newScoreRank);
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
