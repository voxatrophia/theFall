using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class LoadAudioSettings : MonoBehaviour {
    public AudioMixer mainMixer;
    AudioSource audioSrc;

    void Start() {
        if (PlayerPrefs.HasKey("musicVol")) {
            mainMixer.SetFloat("musicVol", PlayerPrefs.GetFloat("musicVol"));
        }
        if (PlayerPrefs.HasKey("sfxVol")) {
            mainMixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("sfxVol"));
        }
    }

    //IEnumerator StartMusic() {
    //    yield return Yielders.Get(0.5f);
    //    audioSrc.Play();
    //}
}
