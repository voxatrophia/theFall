using UnityEngine;
using System.Collections;

public class Ribs : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.CompareTag(Tags.Player)){
            EventManager.TriggerEvent(Events.HealthPickup);
			gameObject.SetActive(false);
		}

	}


}
