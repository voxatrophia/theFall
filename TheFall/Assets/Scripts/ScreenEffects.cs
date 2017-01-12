using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour {

	public Color damageColor;
	public Color healthColor;
	ScreenShake shake;
	ScreenFlash flash;

	void Start(){
		shake = GetComponent<ScreenShake> ();
		flash = GetComponent<ScreenFlash>();
	}

	void OnEnable(){
		EventManager.StartListening(Events.Damage, ScreenEffect);
		EventManager.StartListening(Events.AddHealth, HealthScreenEffect);
	}

	void OnDisable(){
		EventManager.StopListening(Events.Damage, ScreenEffect);
		EventManager.StopListening(Events.AddHealth, HealthScreenEffect);
	}

	void ScreenEffect(){
		shake.DoShake();
		flash.FlashScreen(damageColor);
	}

	void HealthScreenEffect(){
		flash.FlashScreen(healthColor);
	}
}
