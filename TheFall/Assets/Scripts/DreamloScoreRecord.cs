using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DreamloScoreRecord : MonoBehaviour {
    public Text playerName;
    public Text score;
    public Text date;
    public Text platform;
    public Text num;

    public void SetUI(DreamloScore s, int i) {
        playerName.text = s.name.Replace("+", " ");
        score.text = s.score.ToString("N0");
        platform.text = s.shortText;
        date.text = s.dateString.ToShortDateString();
        num.text = i.ToString();
    }
}
