using UnityEngine;
using System.Collections;

//Tutorial Stage
// 1 =  No enemy attacks
//      No Energy Meter or charge attacks
//      Items one at a time
// After all items -> 2
// 
// 2 =  No Enemy Attacks
//      Intro energy Meter
//
// First successful attack -> 3
//
// 3 =  Add Enemy attacks
//      Start increasing difficulty
//
public class TutorialManager : Singleton<TutorialManager>
{
    public bool inTutorial;
    int tutorialStage;

    void Start() {
        inTutorial = true;
        tutorialStage = 1;
        if (inTutorial) {
            EventManager.TriggerEvent("TutorialStage1Start");
        }
    }

    void OnEnable() {
        EventManager.StartListening("TutorialStage1Done", ChangeStage);
        EventManager.StartListening("TutorialStage2Done", ChangeStage);
        EventManager.StartListening("TutorialStage3Done", ChangeStage);
    }

    void OnDisable() {
        EventManager.StopListening("TutorialStage1Done", ChangeStage);
        EventManager.StopListening("TutorialStage2Done", ChangeStage);
        EventManager.StopListening("TutorialStage3Done", ChangeStage);
    }

    public void ChangeStage() {
        Debug.Log("Tutorial Stage Done: Level " + tutorialStage);

        switch (tutorialStage) {
            case 1:
                EventManager.TriggerEvent("TutorialStage2Start");
                break;
            case 2:
                EventManager.TriggerEvent("TutorialStage3Start");
                break;
            default:
                inTutorial = false;
                Debug.Log("Tutorial Done");
                break;
        }

        tutorialStage += 1;

    }
}
