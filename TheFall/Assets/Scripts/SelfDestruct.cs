using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float lifetime = 5f;

	void OnEnable(){
		StartCoroutine("DestructSelf");
	}

	IEnumerator DestructSelf(){
		yield return new WaitForSeconds(lifetime);
		this.gameObject.SetActive(false);
	}
}
