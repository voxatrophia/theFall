using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthChargeManager : MonoBehaviour {

    public Slider HealthMeter;
    public GameObject HealthCharge;

    void Start() {
        HealthMeter.gameObject.SetActive(true);
        HealthCharge.SetActive(true);
    }
}