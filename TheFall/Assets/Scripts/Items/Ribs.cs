using UnityEngine;
using System.Collections;

public class Ribs : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Player"){
            EventManager.TriggerEvent("HealthPickup");

			gameObject.SetActive(false);
		}

	}


}
