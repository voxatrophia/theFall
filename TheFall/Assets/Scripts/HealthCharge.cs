using UnityEngine;
using UnityEngine.UI;

public class HealthCharge : MonoBehaviour {
    public float rechargeRate = 10;
    public float maxEnergy = 100;
    public Slider healthMeter;
    public AudioClip fullChime;

    float energyLevel = 0;
    bool energyFullTrigger = false;
    AudioSource audioSrc;

    bool firstCharge;

    void OnEnable() {
        EventManager.StartListening(Events.Damage, ResetEnergy);
    }

    void OnDisable() {
        EventManager.StopListening(Events.Damage, ResetEnergy);
    }

    void ResetEnergy() {
        energyLevel = 0;
        energyFullTrigger = false;
    }

    void Start() {
        audioSrc = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!energyFullTrigger && other.CompareTag(Tags.Player)) {
            audioSrc.Play();
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag(Tags.Player) && !energyFullTrigger) {
            //Mathf.Min returns smallest of 2 values, so energy level won't ever be above 100
            energyLevel = Mathf.Min(energyLevel + rechargeRate * Time.deltaTime, maxEnergy);
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

    void Update() {
        if (!energyFullTrigger) {
            healthMeter.value = energyLevel;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(Tags.Player)) {
            audioSrc.Stop();
        }
    }
}
