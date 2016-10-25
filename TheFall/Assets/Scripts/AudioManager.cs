using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager> {

	AudioSource effects;
    AudioSource music;
    enum Fade {In, Out};

	void Awake(){
        DontDestroyOnLoad(transform.gameObject);
        foreach (AudioSource aud in GetComponents<AudioSource>()) {
            switch (aud.outputAudioMixerGroup.name) {
                case "Music":
                    music = aud;
                    break;
                case "SoundEffects":
                    effects = aud;
                    break;
            }
        }
    }

	public void SwitchMusic(AudioClip clip){
		music.clip = clip;
		music.Play();
	}

    IEnumerator FadeAudio (float timer, Fade fadeType) {
        float start = fadeType == Fade.In ? 0.0F : 1.0F;
        float end = fadeType == Fade.In ? 1.0F : 0.0F;
        float i = 0.0F;
        float step = 1.0F/timer;
     
        while (i <= 1.0F) {
            i += step * Time.deltaTime;
            music.volume = Mathf.Lerp(start, end, i);
            yield return Yielders.Get(step * Time.deltaTime);
        }
    }

    public void PlaySoundEffect(AudioClip se) {
        effects.PlayOneShot(se);
    }

    public void Pause(){
		if(music.isPlaying){
			music.Pause();
		}
    }

    public void Play(){
		music.Play();
    }

	public void PauseSound(){
		StartCoroutine(StopMusic());
	}

	public void StopSound(){
		music.Stop();
	}
	
	IEnumerator StopMusic(){
		if(music.isPlaying){
			music.Pause();
			yield return Yielders.Get(2f);
			music.Play();
		}
	}
}
