using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "Player"){
			ItemManager.i.SetItem(gameObject);
			gameObject.SetActive(false);
			ItemManager.i.audioSrc.Play();
		}
	}
}
