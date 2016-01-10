using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnergyManager : MonoBehaviour {

    public Slider EnergyMeter;
    public GameObject EnergyCharge;

    void Start() {
        if (TutorialManager.Instance.inTutorial) {
            EnergyMeter.gameObject.SetActive(false);
            EnergyCharge.SetActive(false);
        }
    }

    void OnEnable() {
        EventManager.StartListening(TutorialEvents.AttackStageStart, EnableEnergyCharge);
    }

    void OnDisable() {
        EventManager.StopListening(TutorialEvents.AttackStageStart, EnableEnergyCharge);
    }

    void EnableEnergyCharge() {
        EnergyMeter.gameObject.SetActive(true);
        EnergyCharge.SetActive(true);
    }
}
