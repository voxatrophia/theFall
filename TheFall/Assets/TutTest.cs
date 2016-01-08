using UnityEngine;
using System.Collections;

public class TutTest : MonoBehaviour {

    void OnEnable() {
        EventManager.StartListening("TutorialStage1Start", ChangeStage);
        EventManager.StartListening("TutorialStage2Start", ChangeStage);
        EventManager.StartListening("TutorialStage3Start", ChangeStage);
    }

    void OnDisable() {
        EventManager.StopListening("TutorialStage1Start", ChangeStage);
        EventManager.StopListening("TutorialStage2Start", ChangeStage);
        EventManager.StopListening("TutorialStage3Start", ChangeStage);
    }

    void ChangeStage() {
        Debug.Log("Start Tutorial Stage");
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            EventManager.TriggerEvent("TutorialStage1Done");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            EventManager.TriggerEvent("TutorialStage2Done");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            EventManager.TriggerEvent("TutorialStage3Done");
        }


    }
}
