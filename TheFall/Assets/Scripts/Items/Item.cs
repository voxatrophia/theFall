using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.CompareTag(Tags.Player)){
			ItemManager.i.SetItem(gameObject);
			gameObject.SetActive(false);
		}
	}
}
