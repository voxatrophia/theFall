using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	bool readyToShoot = false;
	public GameObject attack;


	void OnEnable(){
		EventManager.StartListening("EnergyFull", Ready);
		if(attack != null) {
			attack.SetActive(false);
		}
	}

	void OnDisable(){
		EventManager.StopListening("EnergyFull", Ready);
	}

	void Update(){
		if(readyToShoot){
			if(Input.GetButtonDown("Fire2")){
				Shoot();
			}
		}
	}

	void Ready(){
		readyToShoot = true;
	}

	void Shoot(){
		attack.transform.position = transform.position;
		attack.SetActive(true);
        EventManager.TriggerEvent("PlayerAttack");
		readyToShoot = false;
	}

}
