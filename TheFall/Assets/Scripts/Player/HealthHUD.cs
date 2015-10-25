using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthHUD : MonoBehaviour {

	Text text;

	void Start(){
		text = GetComponent<Text> ();
		text.text = "x" + Health.CheckHealth();
	}

	void OnEnable(){
		EventManager.StartListening(Events.Damage, UpdateHealth);
		EventManager.StartListening(Events.AddHealth, UpdateHealth);
	}

	void OnDisable(){
		EventManager.StopListening(Events.Damage, UpdateHealth);
		EventManager.StopListening(Events.AddHealth, UpdateHealth);
	}

	void UpdateHealth(){
		text.text = "x" + Health.CheckHealth();
	}
}