using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour {

	public Vector2 velocity;
	public float speedIncrease = 1.3f;
	Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = velocity;
	}

	void OnEnable(){
		EventManager.StartListening("StopMoving", StopMoving);
		EventManager.StartListening("MoveBackwards", MoveBackwards);
		EventManager.StartListening("BossNearDeath", MoveFaster);
	}

	void OnDisable(){
		EventManager.StopListening("StopMoving", StopMoving);
		EventManager.StopListening("MoveBackwards", MoveBackwards);
		EventManager.StopListening("BossNearDeath", MoveFaster);
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

	void MoveFaster(){
		velocity = new Vector2(0, rb.velocity.y * speedIncrease);
		rb.velocity = velocity;
	}
}
