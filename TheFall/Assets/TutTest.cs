using UnityEngine;
using System.Collections;

public class TutTest : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            EventManager.TriggerEvent("TutorialStage2Done");
        }
    }
}
