using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterName : MonoBehaviour {

    public GameObject namePanel;
    public InputField nameField;

    HighScoreManager hs;

    void Awake() {
        namePanel.SetActive(false);
    }

    public void Start() {
        hs = GetComponent <HighScoreManager> ();
    }

    //called On End Edit of InputField
    public void SaveName() {

        if (nameField.text == "") {
            hs.GetName("Anonymous");
        }
        else {
            hs.GetName(nameField.text);
        }

        namePanel.SetActive(false);
    }
}
