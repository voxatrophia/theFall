using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
public class BossMove : MonoBehaviour {

	public Transform target;
	public float damp = 2f;

	//TODO: When boss gets close, change animation
	//Animator anim;
	//private float proximity;
	private Vector3 velocity = Vector3.zero;

	void Start () {
		//anim = GetComponent<Animator> ();
	}
	
	void Update () {
		//proximity = Vector2.Distance (target.position, transform.position);
		//Send this info to Animator
		//anim.SetFloat ("Proximity", proximity);

		//Only move left and right, lock position.y
		Vector3 dest = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);
		transform.position = Vector3.SmoothDamp (transform.position, dest, ref velocity, damp);
	}
}
