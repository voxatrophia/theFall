using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Vector2 velocity;
	Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0, 3);
	}

	void OnEnable(){
		EventManager.StartListening("StopMoving", StopMoving);
		EventManager.StartListening("MoveBackwards", MoveBackwards);
	}

	void OnDisable(){
		EventManager.StopListening("StopMoving", StopMoving);
		EventManager.StopListening("MoveBackwards", MoveBackwards);
	}

	
	void StopMoving(){
		StartCoroutine("StopMovingCoroutine");
	}

	IEnumerator StopMovingCoroutine(){
		rb.velocity = new Vector2(0, 0);
		yield return new WaitForSeconds(2f);
		rb.velocity = velocity;
	}

	void MoveBackwards(){
		StartCoroutine("MoveBackwardsCoroutine");
	}
	IEnumerator MoveBackwardsCoroutine(){
		rb.velocity = new Vector2(0, -5);
		yield return new WaitForSeconds(1f);
		rb.velocity = velocity;
	}
}
