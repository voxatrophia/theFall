using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager> {

	AudioSource audioSource;
    enum Fade {In, Out};

	void Awake(){
        DontDestroyOnLoad(transform.gameObject);
		audioSource = GetComponent<AudioSource>();
	}

	// public void SwitchMusic(AudioClip clip){
	// 	if(audioSource.isPlaying){
	// 		StartCoroutine(FadeAudio(1f,Fade.Out));
	// 		audioSource.clip = clip;
	// 		audioSource.Play();
	// 	}
	// 	else{
	// 		audioSource.clip = clip;
	// 		audioSource.Play();
	// 	}
	// }

	public void SwitchMusic(AudioClip clip){
		audioSource.clip = clip;
		audioSource.Play();
	}

    IEnumerator FadeAudio (float timer, Fade fadeType) {
        float start = fadeType == Fade.In? 0.0F : 1.0F;
        float end = fadeType == Fade.In? 1.0F : 0.0F;
        float i = 0.0F;
        float step = 1.0F/timer;
     
        while (i <= 1.0F) {
            i += step * Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, end, i);
            yield return Yielders.Get(step * Time.deltaTime);
        }
    }

    public void Pause(){
		if(audioSource.isPlaying){
			audioSource.Pause();
		}
    }

    public void Play(){
		audioSource.Play();
    }

	public void PauseSound(){
		StartCoroutine(StopMusic());
	}

	public void StopSound(){
		audioSource.Stop();
	}
	
	IEnumerator StopMusic(){
		if(audioSource.isPlaying){
			audioSource.Pause();
			yield return Yielders.Get(2f);
			audioSource.Play();
		}
	}
}
