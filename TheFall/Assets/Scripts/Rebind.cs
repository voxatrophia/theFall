using UnityEngine;
using UnityEngine.UI;

public class Rebind : Menu {
    public Text action;
    public actions control;

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
