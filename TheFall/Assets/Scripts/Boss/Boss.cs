using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Animator))]
public class Boss : MonoBehaviour {
	public GameObject[] attacks;
	//Animator anim;

	void Start () {
		//anim = GetComponent<Animator> ();
		StartCoroutine("Attack");
	}
	
	IEnumerator Attack(){
		while(true){
			yield return new WaitForSeconds(Random.Range(3,5));
			if(attacks != null){
				Instantiate(attacks[Random.Range (0, attacks.GetLength(0))], transform.position, Quaternion.identity);
			}
		}
	}
}

