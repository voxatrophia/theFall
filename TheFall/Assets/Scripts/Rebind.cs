using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum actions { Jump, UseItem, Pause, Back }

public class Rebind : Menu {
    public Text action;
    public actions control;

    //void Start() {
    //    gameObject.SetActive(false);
    //}

    void OnEnable() {
        action.text = control.ToString();
    }

    public void CloseWindow() {
        InputHandler.Instance.ReturnControlHelper();
    }

    void Update() {
        //Get every possible value for KeyCode
        System.Array values = System.Enum.GetValues(typeof(KeyCode));
        //Loop through all possible values and see if any of them are pressed
        foreach (KeyCode code in values) {
            if (Input.GetKeyDown(code)) {
                //Set Key - Will return true if set
                if (Controls.Instance.SetKey(control, code)) {
                    Debug.Log(control.ToString() + " Set to " + code);
                }
                //Will return false if that key is already in use
                else {
                    InputHandler.Instance.KeyInUse();
                }
                InputHandler.Instance.ReturnControlHelper();
                gameObject.SetActive(false);
                break;
            }
        }
    }
}
