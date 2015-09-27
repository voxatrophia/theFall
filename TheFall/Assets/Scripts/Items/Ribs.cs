using UnityEngine;
using System.Collections;

public class Ribs : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Player"){
			Health.AddHealth();
			Destroy(this.gameObject);
		}

	}


}
