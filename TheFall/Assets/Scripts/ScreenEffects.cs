using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour {

	public Color damageColor;
	public Color healthColor;
	CameraShake shake;
	ScreenFlash flash;

	void Start(){
		shake = GetComponent<CameraShake> ();
		flash = GetComponent<ScreenFlash>();
	}

	void OnEnable(){
		EventManager.StartListening("Damage", ScreenEffect);
		EventManager.StartListening("AddHealth", HealthScreenEffect);
	}

	void OnDisable(){
		EventManager.StopListening("Damage", ScreenEffect);
		EventManager.StopListening("AddHealth", HealthScreenEffect);
	}

	void ScreenEffect(){
		shake.DoShake(0.3f);
		flash.FlashScreen(damageColor);
	}

	void HealthScreenEffect(){
		flash.FlashScreen(healthColor);
	}
}
