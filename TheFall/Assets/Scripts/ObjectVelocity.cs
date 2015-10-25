using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectVelocity : MonoBehaviour {

	public Vector2 velocity;
	Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = velocity;
	}
}
