using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour {
	public GameObject[] platforms;
	public float platformDistance = 1f;
	private Vector3 origin;
	public Transform MovingPlatform;

	float[] weights;

	void Start () {
		platforms = new GameObject[6];
		weights = new float[6];
		if(platforms[0] != null){
			Debug.Log(platforms[0].name);

		}
		origin = transform.position;

		weights[0] = 0.1f;
		weights[1] = 0.2f;
		weights[2] = 0.3f;
		weights[3] = 0.3f;
		weights[4] = 0.2f;
		weights[5] = 0.1f;

		Spawn();
	}
	
	void Update () {
		if((transform.position.y + platformDistance) < (origin.y)){
			Spawn();
		}
	}
	void Spawn(){
		Instantiate(platforms[Random.Range (0, platforms.GetLength(0))], transform.position, Quaternion.identity);

		// GameObject newPlatform;
		// newPlatform = Instantiate(platforms[Random.Range (0, platforms.GetLength(0))], transform.position, Quaternion.identity) as GameObject;
		// Debug.Log(newPlatform.name);
		// newPlatform.transform.parent = MovingPlatform;
		// origin = transform.position;
	}
}