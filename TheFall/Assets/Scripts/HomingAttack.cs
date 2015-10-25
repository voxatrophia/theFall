using UnityEngine;
using System.Collections;

public class HomingAttack : MonoBehaviour {

	public float maxSpeed = 10f;
	public float nearSpeed = 5f;

	GameObject target;
	float curSpeed;
	bool canMove;

	void OnEnable(){
		canMove = true;
		EventManager.StartListening(Events.StopMoving, StopMoving);
	}

	void OnDisable(){
		EventManager.StopListening(Events.StopMoving, StopMoving);
	}

	void Start(){
		target = GameObject.FindWithTag(Tags.Player);
	}

	//Coroutine wrapper
	void StopMoving(){
		StartCoroutine(StopMovingCoroutine());
	}

	IEnumerator StopMovingCoroutine(){
		canMove = false;
		yield return Yielders.Get(2f);
		canMove = true;
	}

	void Update () {
		//Reduce speed when the attack gets closer to the player, makes it more manageable
		curSpeed = (Vector3.Distance(target.transform.position, transform.position) < 3) ? nearSpeed : maxSpeed;

		if(canMove){
		    transform.position = Vector3.MoveTowards (transform.position, target.transform.position, Time.deltaTime * curSpeed);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(Tags.Player)){
			this.gameObject.SetActive(false);
		}
	}
}
