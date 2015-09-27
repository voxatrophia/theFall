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
		EventManager.StartListening("Damage", UpdateHealth);
		EventManager.StartListening("AddHealth", UpdateHealth);
	}

	void OnDisable(){
		EventManager.StopListening("Damage", UpdateHealth);
		EventManager.StartListening("AddHealth", UpdateHealth);
	}

	void UpdateHealth(){
		text.text = "x" + Health.CheckHealth();
	}

}
