using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(MultiObjectPooler))]
public class BossAttack : MonoBehaviour {
	public float attackMinTime = 3f;
	public float attackMaxTime = 5f;

	Animator anim;
	MultiObjectPooler attacks;

	void Start () {
		anim = GetComponent<Animator> ();
		attacks = GetComponent<MultiObjectPooler>();
		StartCoroutine("Attack");
	}

	IEnumerator Attack(){
		while(true){
			//random Attack interval
			yield return new WaitForSeconds(Random.Range(attackMinTime,attackMaxTime));

			GameObject attack = attacks.GetPooledObjectOfRandomType();
			if(attack != null){
				attack.transform.position = transform.position;
				attack.transform.rotation = Quaternion.identity;
				attack.SetActive(true);
				anim.SetTrigger("Attack");
			}
		}
	}
}
