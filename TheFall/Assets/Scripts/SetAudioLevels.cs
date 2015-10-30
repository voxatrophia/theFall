using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLevels : MonoBehaviour {
	public AudioMixer mainMixer;
	public Slider musicSlider;
	public Slider sfxSlider;

	void Start(){
		if(PlayerPrefs.HasKey("musicVol")){
			mainMixer.SetFloat("musicVol", PlayerPrefs.GetFloat("musicVol"));
			musicSlider.value = PlayerPrefs.GetFloat("musicVol");
		}
		if(PlayerPrefs.HasKey("sfxVol")){
			mainMixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("sfxVol"));
			sfxSlider.value = PlayerPrefs.GetFloat("sfxVol");
		}

	}

	public void SetMusicLevel(float musicVol){
		if(musicVol < -24f){
			musicVol = -80;
		}
		mainMixer.SetFloat("musicVol", musicVol);
		PlayerPrefs.SetFloat("musicVol", musicVol);
	}

	public void SetSfxLevel(float sfxVol){
		if(sfxVol < -24f){
			sfxVol = -80;
		}
		mainMixer.SetFloat("sfxVol", sfxVol);
		PlayerPrefs.SetFloat("sfxVol", sfxVol);
	}

}
