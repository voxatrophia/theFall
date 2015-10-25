using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Gwen : MonoBehaviour {
	public float talkTime = 5f;

	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
		StartCoroutine(Talk());

	}

	IEnumerator Talk(){
		yield return Yielders.Get(talkTime);
		anim.SetBool("Done", true);
	}	

}
