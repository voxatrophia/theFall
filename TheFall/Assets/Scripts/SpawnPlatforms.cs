using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour {
	public GameObject[] platforms;
	public float platformDistance = 3f;
	public Transform movingPlatform;

	GameObject newPlatform;
//	float[] weights;

	void Start () {
//		platforms = new GameObject[6];
//		weights = new float[6];

		// weights[0] = 0.1f;
		// weights[1] = 0.2f;
		// weights[2] = 0.3f;
		// weights[3] = 0.3f;
		// weights[4] = 0.2f;
		// weights[5] = 0.1f;

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
		newPlatform = Instantiate(platforms[Random.Range (0, platforms.GetLength(0))],
								  new Vector3 (transform.position.x, transform.position.y, movingPlatform.position.z),
								  Quaternion.identity)
					  as GameObject;
		newPlatform.transform.parent = movingPlatform;
	}
}