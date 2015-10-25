using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(MultiObjectPooler))]
public class BossAttack : MonoBehaviour {
	public float attackMinTime = 3f;
	public float attackMaxTime = 5f;

	bool canMove = true;
	GameObject attack;
	Animator anim;
	MultiObjectPooler attacks;

	void Start () {
		anim = GetComponent<Animator> ();
		attacks = GetComponent<MultiObjectPooler>();
		StartCoroutine(Attack());
	}

	void OnEnable(){
		//Called from Stopwatch item
		EventManager.StartListening(Events.StopMoving, StopMoving);
	}

	void OnDisable(){
		EventManager.StopListening(Events.StopMoving, StopMoving);
	}

	IEnumerator Attack(){
		while(true){
			yield return Yielders.Get((Random.Range(attackMinTime,attackMaxTime)));

			if(canMove){
				attack = attacks.GetPooledObjectOfRandomType();
				if(attack != null){
					attack.transform.position = transform.position;
					attack.transform.rotation = Quaternion.identity;
					attack.SetActive(true);
					anim.SetTrigger(BossAnim.Attack);
				}
			}
		}
	}

	void StopMoving(){
		StartCoroutine(StopMovingCoroutine());
	}

	IEnumerator StopMovingCoroutine(){
		canMove = false;
		yield return new WaitForSeconds(2f);
		canMove = true;
	}

}
