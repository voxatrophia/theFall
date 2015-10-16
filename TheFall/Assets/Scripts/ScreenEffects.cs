using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour {

	public Color damageColor;
	public Color healthColor;
//	CameraShake shake;
	ScreenShake shake;
	ScreenFlash flash;
	AudioSource audioSrc;

	void Start(){
		audioSrc = GetComponent<AudioSource>();
//		shake = GetComponent<CameraShake> ();
		shake = GetComponent<ScreenShake> ();
		flash = GetComponent<ScreenFlash>();
	}

	void OnEnable(){
		EventManager.StartListening("Damage", ScreenEffect);
		EventManager.StartListening("AddHealth", HealthScreenEffect);
		EventManager.StartListening("StopMoving", PauseSound);
	}

	void OnDisable(){
		EventManager.StopListening("Damage", ScreenEffect);
		EventManager.StopListening("AddHealth", HealthScreenEffect);
		EventManager.StopListening("StopMoving", PauseSound);
	}

	void ScreenEffect(){
		shake.DoShake(0.3f);
		flash.FlashScreen(damageColor);
	}

	void HealthScreenEffect(){
		flash.FlashScreen(healthColor);
	}

	void PauseSound(){
		StartCoroutine("StopMusic");
	}
	
	IEnumerator StopMusic(){
		if(audioSrc.isPlaying){
			audioSrc.Pause();
			yield return new WaitForSeconds(2f);
			audioSrc.Play();
		}
	}
}
