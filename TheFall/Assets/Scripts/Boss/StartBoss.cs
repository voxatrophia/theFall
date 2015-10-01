using UnityEngine;
using System.Collections;

public class StartBoss : MonoBehaviour {

	Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
		anim.SetFloat("Proximity", 25f);
	}

	IEnumerator RandomAnim(){
		while(true){
			yield return new WaitForSeconds(Random.Range(3,5));
			if(Random.value > 0.5f){
				anim.Play("Attack");
			}
			else{
				anim.Play("Talk");
			}			
		}
	}
}
