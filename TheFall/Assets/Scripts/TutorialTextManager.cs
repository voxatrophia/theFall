using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialTextManager : MonoBehaviour {
    //public Text ItemText;
    //public Text ChargeText;
    //public Text AttackText;

    //public float fadeInTime = 2f;
    //public float fadeOutTime = 10f;
    //public float displayTime = 3f;

    //void OnEnable() {
    //    //Triggered in UseItem
    //    EventManager.StartListening(TutorialEvents.FirstItem, ShowItemText);
    //    //Triggered in TutorialManager
    //    EventManager.StartListening(TutorialEvents.AttackStageStart, ShowEnergyChargeText);
    //    //Triggered in EnergyCharge
    //    EventManager.StartListening(TutorialEvents.EnergyFull, ShowEnergyAttackText);
    //}

    //void OnDisable() {
    //    EventManager.StopListening(TutorialEvents.FirstItem, ShowItemText);
    //    EventManager.StopListening(TutorialEvents.AttackStageStart, ShowEnergyChargeText);
    //    EventManager.StopListening(TutorialEvents.EnergyFull, ShowEnergyAttackText);
    //}

    //void Start() {
    //    Initialize(ItemText);
    //    Initialize(ChargeText);
    //    Initialize(AttackText);
    //}

    //void Initialize(Text TutorialText) {
    //    //Must set CanvasRenderer to 0 get get CrossFadeAlpha to work
    //    //But it also makes the text invisible to start which is necessary
    //    TutorialText.canvasRenderer.SetAlpha(0.0f);
    //    TutorialText.gameObject.SetActive(false);
    //}

    //IEnumerator DisplayText(Text display) {
    //    display.gameObject.SetActive(true);
    //    display.CrossFadeAlpha(1f, fadeInTime, true);
    //    yield return Yielders.Get(displayTime);
    //    display.CrossFadeAlpha(0, fadeOutTime, true);
    //}

    //void ShowItemText() {
    //    StartCoroutine(DisplayText(ItemText));
    //}

    //void ShowEnergyChargeText() {
    //    StartCoroutine(DisplayText(ChargeText));
    //}

    //void ShowEnergyAttackText() {
    //    StartCoroutine(DisplayText(AttackText));
    //}
}
