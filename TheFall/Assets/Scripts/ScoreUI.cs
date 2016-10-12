using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScoreUI : MonoBehaviour {

    public Text playerName;
    public Text score;
    public Text date;
    public Text number;
    public Image background;
    public string nameEntered;
    public InputField nameField;

    public void Start() {
//        nameField.gameObject.SetActive(false);
    }

    public void SetUI(string player, int scr, DateTime dt, int num) {
        number.text = num.ToString();
        playerName.text = player;
        score.text = scr.ToString("N0");
        date.text = dt.ToShortDateString();
    }

    public void SetUI(int num) {
        number.text = num.ToString();
        playerName.text = "-";
        score.text = "-";
        date.text = "-";
    }

    public void EnterName(GameObject field) {
        nameEntered = nameField.text;
        playerName.text = nameEntered;
        field.SetActive(false);
    }
}
