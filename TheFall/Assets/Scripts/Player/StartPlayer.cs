using UnityEngine;
using System.Collections;

public class StartPlayer : MonoBehaviour {
	Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
		anim.SetBool(PlayerAnim.Start, true);
	}
}
