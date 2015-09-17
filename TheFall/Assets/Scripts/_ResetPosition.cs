using UnityEngine;
using System.Collections;

public class _ResetPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			Debug.Log("Collide");
			other.gameObject.transform.position = new Vector2(0f,2f);
		}
	}
}
