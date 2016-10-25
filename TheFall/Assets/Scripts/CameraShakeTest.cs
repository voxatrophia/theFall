using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraShakeTest : MonoBehaviour {

    float intensity;
    float decay;

    public InputField intField;
    public InputField decayField;

    ScreenShake shaker;

    void Start() {
        shaker = GetComponent<ScreenShake>();
        Debug.Log("Shake Intensity: " + shaker.shakeIntensity);
        Debug.Log("Shake Decay: " + shaker.shakeDecay);

        intField.text = shaker.shakeIntensity.ToString();
        decayField.text = shaker.shakeDecay.ToString();

    }

    public void SetIntensity() {
        float.TryParse(intField.text, out intensity);
        shaker.shakeIntensity = intensity;
        Debug.Log("Shake Intensity: " + shaker.shakeIntensity);
    }

    public void SetDecay() {
        float.TryParse(decayField.text, out decay);
        shaker.shakeDecay = decay;
        Debug.Log("Shake Decay: " + shaker.shakeDecay);
    }

}
