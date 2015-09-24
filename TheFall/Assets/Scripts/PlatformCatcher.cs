using UnityEngine;
using System.Collections;

public class PlatformCatcher : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
//		Debug.Log("Collide" + col.gameObject.name);
		if(col.gameObject.tag == "Platform" || col.gameObject.tag == "Item"){
			Destroy(col.gameObject);
		}		
	}

}
