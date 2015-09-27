using UnityEngine;
using System.Collections;

public class ItemSpanwer : MonoBehaviour {

	public GameObject[] items;

	void Start(){
		InvokeRepeating("SpawnItem", 3, Random.Range(3,5));
	}

	void SpawnItem(){
		Vector3 loc = new Vector3(Random.Range(-10,10), transform.position.y, transform.position.z);
		Instantiate(items[Random.Range(0,items.Length)], loc, Quaternion.identity);
	}

}
