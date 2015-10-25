using UnityEngine;
using System.Collections;

public class BoxTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Messenger.Broadcast("Test");
		}
	}

}
