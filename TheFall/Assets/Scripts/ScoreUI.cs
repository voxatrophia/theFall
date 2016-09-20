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
        playerName.text = player;
        score.text = scr.ToString();
        date.text = dt.ToShortDateString();
        number.text = num.ToString();
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
