using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnergyCharge : MonoBehaviour {

	float energyLevel = 0;
	float rechargeRate = 25;
	public Slider energy;
	bool energyFullTrigger = false;
	AudioSource audioSrc;

	void OnEnable(){
		EventManager.StartListening("Damage", ResetEnergy);
		EventManager.StartListening("PlayerAttack", ResetEnergy);
	}

	void OnDisable(){
		EventManager.StopListening("Damage", ResetEnergy);
		EventManager.StopListening("PlayerAttack", ResetEnergy);
	}

	void ResetEnergy(){
		energyLevel = 0;
		energyFullTrigger = false;
	}


	void Start(){
		if(energy == null){
			Debug.LogError("Slider not attached");
		}
		audioSrc = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			audioSrc.Play();
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag == "Player" && !energyFullTrigger){
			energyLevel = Mathf.Min(energyLevel + rechargeRate * Time.deltaTime, 100.0F);
			if(energyLevel == 100){
				energyFullTrigger = true;
		        EventManager.TriggerEvent("EnergyFull");
			}
		}
	}

	void Update(){
		if(!energyFullTrigger){
			energy.value = energyLevel;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Player")){
			audioSrc.Stop();
		}		
	}

}
