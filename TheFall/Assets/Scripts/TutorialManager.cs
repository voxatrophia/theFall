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
// First successful attack -> Done
//
// Done =  Add Enemy attacks
//      Start increasing difficulty
//      Normal gameplay starts here

public class TutorialManager : Singleton<TutorialManager>
{
    public bool inTutorial;
    int tutorialStage;

    void Awake() {
//        inTutorial = false;
        tutorialStage = 1;
    }

    void Start() {
        //Will get inTutorial from PlayerPrefs so only have to see the tutorial once
        //Also have an option to start there

        //if (inTutorial) {
        //    EventManager.TriggerEvent("TutorialStage1Start");
        //}
    }

    void OnEnable() {
        //Triggered in ItemSpawner
        EventManager.StartListening(TutorialEvents.ItemStageDone, ChangeStage);
        //Triggered in Bosshealth
        EventManager.StartListening(TutorialEvents.AttackStageDone, ChangeStage);
    }

    void OnDisable() {
        EventManager.StopListening(TutorialEvents.ItemStageDone, ChangeStage);
        EventManager.StopListening(TutorialEvents.AttackStageDone, ChangeStage);
    }

    public void ChangeStage() {

        switch (tutorialStage) {
            case 1:
                tutorialStage = 2;
                EventManager.TriggerEvent(TutorialEvents.AttackStageStart);
                break;
            case 2:
                tutorialStage = 3;
                EventManager.TriggerEvent(TutorialEvents.Done);
                break;
            default:
                inTutorial = false;
                Debug.Log("Tutorial Done");
                break;
        }
    }
}
