using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Light))]
public class LightPulse : MonoBehaviour {
	private Light lt;
    public bool startOn;
    public float maxBrightness;
    public float transitionTime;
    public float peakDelay;
 	public float minBrightness;

    private bool isLit;

    void Awake(){
		lt = GetComponent<Light>();
        isLit = startOn;
        lt.intensity = isLit ? maxBrightness : minBrightness;
    }

    void OnEnable()
    {
       StartCoroutine(Transition(!isLit));
    }
 
    void OnDisable()
    {
       StopAllCoroutines();
       isLit = false;
       lt.intensity = 0;
    }

    IEnumerator Transition(bool turnOn){
        float initialBrightness = lt.intensity;
        float targetBrightness = turnOn ? maxBrightness : minBrightness;
        float startTime = Time.time;
        float endTime = startTime + transitionTime;

        while (endTime >= Time.time)
        {
            lt.intensity = Mathf.Lerp(initialBrightness, targetBrightness, (Time.time - startTime)/transitionTime);
            yield return null;
        }

        isLit = turnOn;
        yield return new WaitForSeconds(peakDelay);
        StartCoroutine(Transition(!isLit));
     }
}