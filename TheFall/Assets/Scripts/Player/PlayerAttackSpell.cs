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
}
