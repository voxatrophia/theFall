using UnityEngine;
using System.Collections;

public class GravitySpell : MonoBehaviour {

	Rigidbody2D rb;
	float origGravity;
	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		origGravity = rb.gravityScale;
	}

	void OnEnable(){
		EventManager.StartListening("StopMoving", StopMoving);
		rb.gravityScale = origGravity;
	}

	void OnDisable(){
		EventManager.StopListening("StopMoving", StopMoving);
	}

	void StopMoving(){
		StartCoroutine("StopMovingCoroutine");
	}

	IEnumerator StopMovingCoroutine(){
		rb.gravityScale = 0;
		rb.velocity = new Vector2(0, 0);
		yield return new WaitForSeconds(2f);
		rb.gravityScale = origGravity;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			this.gameObject.SetActive(false);
		}
	}
}