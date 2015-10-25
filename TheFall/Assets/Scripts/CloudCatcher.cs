using UnityEngine;
using System.Collections;

public class CloudCatcher : MonoBehaviour {

	public float min = 1.0f;
	public float max = 4.0f;

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(Tags.Cloud)){
			Vector3 pos = other.transform.position;
			pos.x = -25f;
			pos.y = Random.Range(min, max);
			other.transform.position = pos;
		}
	}
}
