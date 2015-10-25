using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float lifetime = 5f;

	void OnEnable(){
		StartCoroutine(DestructSelf());
	}

	IEnumerator DestructSelf(){
		yield return Yielders.Get(lifetime);
		this.gameObject.SetActive(false);
	}
}
