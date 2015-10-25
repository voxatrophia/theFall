using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLevels : MonoBehaviour {
	public AudioMixer mainMixer;

	public void SetMusicLevel(float musicLvl){
		if(musicLvl < -24f){
			musicLvl = -80;
		}
		mainMixer.SetFloat("musicVol", musicLvl);
	}

	public void SetSfxLevel(float sfxLvl){
		if(sfxLvl < -24f){
			sfxLvl = -80;
		}
		mainMixer.SetFloat("sfxVol", sfxLvl);
	}

}
