using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthCharge : MonoBehaviour {
    public float rechargeRate = 10;
    public float maxEnergy = 100;
    public Slider healthMeter;
    public AudioClip fullChime;

    float energyLevel = 0;
    AudioSource audioSrc;

    bool firstCharge;

    bool SEpaused = false;

    void OnEnable() {
        EventManager.StartListening(Events.Damage, ResetEnergy);
        EventManager.StartListening("GamePaused", ToggleSE);
        EventManager.StartListening("UnPaused", ToggleSE);
    }

    void OnDisable() {
        EventManager.StopListening(Events.Damage, ResetEnergy);
        EventManager.StopListening("GamePaused", ToggleSE);
        EventManager.StopListening("UnPaused", ToggleSE);
    }

    void ResetEnergy() {
        energyLevel = 0;
        healthMeter.value = energyLevel;
    }

    void Start() {
        audioSrc = GetComponent<AudioSource>();
        //audioSrc.Play();
        //audioSrc.Pause();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Tags.Player)) {
            //audioSrc.UnPause();
            audioSrc.Play();
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag(Tags.Player)) {
            //Mathf.Min returns smallest of 2 values, so energy level won't ever be above 100
            energyLevel = Mathf.Min(energyLevel + rechargeRate * Time.deltaTime, maxEnergy);
            healthMeter.value = energyLevel;
            if (energyLevel == maxEnergy) {
                ResetEnergy();
                EventManager.TriggerEvent(Events.HealthPickup);
                audioSrc.PlayOneShot(fullChime);
                //energyFullTrigger = true;
                //audioSrc.Stop();
                //EventManager.TriggerEvent(Events.EnergyFull);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(Tags.Player)) {
            audioSrc.Stop();
            //StartCoroutine(FadeAudio(0.1f));
        }
    }

    void ToggleSE() {
        if (SEpaused) {
            audioSrc.Play();
            SEpaused = false;
        }
        else if (audioSrc.isPlaying) {
            SEpaused = true;
            audioSrc.Pause();
        }
    }

    IEnumerator FadeAudio(float timer) {
        float start = 1;
        float end = 0;
        float i = 0;
        float step = 1.0F / timer;

        while (i <= 1.0F) {
            i += step * Time.deltaTime;
            audioSrc.volume = Mathf.Lerp(start, end, i);
            yield return Yielders.Get(step * Time.deltaTime);
        }
        audioSrc.Stop();
        audioSrc.volume = 1;
    }

}
