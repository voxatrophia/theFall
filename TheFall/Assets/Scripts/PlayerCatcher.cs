using UnityEngine;
using System.Collections;

public class PlayerCatcher : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(Tags.Player)){
	        EventManager.TriggerEvent(Events.MoveBackwards);
		}
	}

}
