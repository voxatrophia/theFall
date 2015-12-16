using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public GameObject attack;
	public AudioClip readySound;

	AudioSource audioSrc;
	bool readyToShoot = false;

	void Start(){
		audioSrc = GetComponent<AudioSource>();
	}

	void OnEnable(){
		EventManager.StartListening(Events.EnergyFull, Ready);
		EventManager.StartListening(Events.Damage, NotReady);

		if(attack != null) {
			attack.SetActive(false);
		}
	}

	void OnDisable(){
		EventManager.StopListening(Events.EnergyFull, Ready);
		EventManager.StopListening(Events.Damage, NotReady);
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
		audioSrc.PlayOneShot(readySound);
	}

	void NotReady(){
		readyToShoot = false;
	}

	void Shoot(){
		attack.transform.position = transform.position;
		attack.SetActive(true);
        EventManager.TriggerEvent(Events.PlayerAttack);
		readyToShoot = false;
	}

}
