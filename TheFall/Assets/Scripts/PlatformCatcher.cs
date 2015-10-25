using UnityEngine;
using System.Collections;

public class PlatformCatcher : MonoBehaviour {

	string[] objects;

	void Start(){
		objects = new string[5];
		objects[0] = Tags.MovingPlatform;
		objects[1] = Tags.Bomb;
		objects[2] = Tags.Ribs;
		objects[3] = Tags.Apple;
		objects[4] = Tags.Stopwatch;
	}

	void OnTriggerEnter2D(Collider2D col) {
		for(int i=0;i<objects.Length;i++) {
			if(col.CompareTag(objects[i])) {
				col.gameObject.SetActive(false);
			}
		}
	}
}
