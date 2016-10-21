using UnityEngine;
using System.Collections;

public class PulseSize : MonoBehaviour {

    public Vector3 startSize;
    public Vector3 endSize;
    public float transitionTime = 2;
    public float peakDelay = 0.5f;
    bool isPulsing = false;

    public void Start() {
        StartCoroutine(Pulse(false));
    }

    IEnumerator Pulse(bool pulsing) {
        Vector3 initialSize = transform.localScale;
        Vector3 targetSize = pulsing ? startSize : endSize;
        float startTime = Time.time;
        float endTime = startTime + transitionTime;

        while (endTime >= Time.time) {
            transform.localScale = Vector3.Lerp(initialSize, targetSize, (Time.time - startTime) / transitionTime);
            yield return null;
        }

        isPulsing = pulsing;
        yield return new WaitForSeconds(peakDelay);
        StartCoroutine(Pulse(!isPulsing));
    }
}
