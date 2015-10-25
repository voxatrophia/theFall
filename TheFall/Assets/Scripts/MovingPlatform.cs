using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour {

	public Vector2 velocity;
	public float speedIncrease = 1.5f;
	Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = velocity;
	}

	void OnEnable(){
		EventManager.StartListening(Events.StopMoving, StopMoving);
		EventManager.StartListening(Events.MoveBackwards, MoveBackwards);
		EventManager.StartListening(Events.BossNearDeath, MoveFaster);
	}

	void OnDisable(){
		EventManager.StopListening(Events.StopMoving, StopMoving);
		EventManager.StopListening(Events.MoveBackwards, MoveBackwards);
		EventManager.StopListening(Events.BossNearDeath, MoveFaster);
	}

	
	void StopMoving(){
		StartCoroutine(StopMovingCoroutine());
	}

	IEnumerator StopMovingCoroutine(){
		rb.velocity = new Vector2(0, 0);
		yield return Yielders.Get(2f);
		rb.velocity = velocity;
	}

	void MoveBackwards(){
		StartCoroutine(MoveBackwardsCoroutine());
	}

	IEnumerator MoveBackwardsCoroutine(){
		rb.velocity = new Vector2(0, -rb.velocity.y);
		yield return Yielders.Get(1f);
		rb.velocity = velocity;
	}

	void MoveFaster(){
		velocity = new Vector2(0, rb.velocity.y * speedIncrease);
		rb.velocity = velocity;
	}
}
