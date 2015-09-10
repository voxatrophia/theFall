using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Vector2 velocity;
	Rigidbody2D rb;
	public float switchTime = 0f;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Start(){
		InvokeRepeating("switchPlatform",switchTime,switchTime);
	}
	
	void Update () {
		rb.velocity = velocity;
	
	}

	void switchPlatform(){
		velocity *= -1;
	}
}
