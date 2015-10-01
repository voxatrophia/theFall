using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(MultiObjectPooler))]
public class Explosion : MonoBehaviour {

	GameObject smoke;
	GameObject fire;
	MultiObjectPooler effects;

	void Start(){
		effects = GetComponent<MultiObjectPooler>();
	}

	public void Explode(Vector3 location){
		if(effects != null){
			smoke = effects.GetPooledObject("Smoke");
			fire = effects.GetPooledObject("Fire");

			if((smoke != null) && (fire != null)) {
				smoke.transform.position = location;
				fire.transform.position = location;
				smoke.SetActive(true);
				fire.SetActive(true);
			}
		}
	}

}
