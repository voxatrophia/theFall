using UnityEngine;
using System.Collections;

public class PlatformCatcher : MonoBehaviour {

	string[] objects;

	void Start(){
		objects = new string[5];
		objects[0] = "MovingPlatform";
		objects[1] = "Bomb";
		objects[2] = "Ribs";
		objects[3] = "Apple";
		objects[4] = "Orb";
	}

	void OnTriggerEnter2D(Collider2D col) {
//		Debug.Log("Collide" + col.gameObject.name);
		foreach(string obj in objects){
			if(col.gameObject.tag == obj){
				Destroy(col.gameObject);
				break;
			}
		}
	}

}
