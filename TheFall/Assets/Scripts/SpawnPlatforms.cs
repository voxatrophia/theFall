using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour {

	public float platformDistance = 3f;
	public Transform movingPlatform;
	MultiObjectPooler platforms;

	GameObject newPlatform;

	void Start () {
		platforms = GetComponent<MultiObjectPooler>();
		Spawn();
	}

	void Update () {
		if(newPlatform != null){
			if((newPlatform.transform.position.y) > (transform.position.y + platformDistance)) {
				Spawn();
			}

		}
	}
	void Spawn(){
		newPlatform = platforms.GetPooledObjectOfRandomType();
		if(newPlatform != null){
			Vector3 pos = transform.position;
			pos.z = movingPlatform.position.z;
			newPlatform.transform.position = pos;
			newPlatform.transform.parent = movingPlatform;
			newPlatform.SetActive(true);
		}
	}
}