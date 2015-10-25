using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerAttackSpell : MonoBehaviour {

	public Vector2 velocity;
	Rigidbody2D rb;

	void OnEnable(){
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = velocity;
	}

	//This disables attack when it hits the Boss...
	//But it also disables the attack when it hits another spell.
	//Nearly impossible to hit boss with this enabled
	// void OnTriggerEnter2D(Collider2D other){
	// 	if(other.CompareTag("Enemy")){
	// 		this.gameObject.SetActive(false);
	// 	}
	// }
}
