using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterName : MonoBehaviour {

    public GameObject namePanel;
    public InputField nameField;

    HighScoreUI hs;

    void Awake() {
        namePanel.SetActive(false);
    }

    void Start() {
        hs = GetComponent <HighScoreUI> ();
    }

    //called On End Edit of InputField
    public void SaveName() {
        if (nameField.text == "") {
            hs.SetName("Anonymous");
        }
        else {
            hs.SetName(nameField.text);
        }
        namePanel.SetActive(false);
    }
}
