using UnityEngine;
using System.Collections;

public class CloudCatcher : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Vector3 pos = other.transform.position;
		pos.x = -25f;
		pos.y = Random.Range(1.0f, 4.0f);
		other.transform.position = pos;
	}
}
