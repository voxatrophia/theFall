using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Vector2 velocity;
	Rigidbody2D rb;
	public bool changeDirection;
	public float switchTime = 3f;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Start(){
		if(changeDirection){
			InvokeRepeating("switchPlatform",switchTime,switchTime);
		}
	}
	
	void Update () {
		rb.velocity = velocity;
	
	}

	void switchPlatform(){
		velocity *= -1;
	}
}
