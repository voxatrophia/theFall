using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MultiObjectPooler))]
public class SpawnPlatforms : MonoBehaviour {

	public float platformDistance = 3f;
	public Transform movingPlatform;
	MultiObjectPooler platforms;

	GameObject newPlatform;
	//this is for a graphics bug
	//adds spacing for new platforms
	int sortingSpacer = 5;

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
			SpriteRenderer spr = newPlatform.GetComponent<SpriteRenderer>();
			spr.sortingOrder = spr.sortingOrder + sortingSpacer;
			sortingSpacer += 5;
			Vector3 pos = transform.position;
			pos.z = movingPlatform.position.z;
			newPlatform.transform.position = pos;
			newPlatform.transform.parent = movingPlatform;
			newPlatform.SetActive(true);
		}
	}
}