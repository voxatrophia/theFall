using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
public class BossMove : MonoBehaviour {

	public Transform target;
	public float damp = 2f;
	bool canMove = true;

	Animator anim;
	private float proximity;
	private Vector3 velocity = Vector3.zero;

	void OnEnable(){
		//Called from Stopwatch item
		EventManager.StartListening(Events.StopMoving, StopMoving);
	}

	void OnDisable(){
		EventManager.StopListening(Events.StopMoving, StopMoving);
	}

	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		if(canMove){
			//Only move left and right, lock position.y
			Vector3 dest = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);
			transform.position = Vector3.SmoothDamp (transform.position, dest, ref velocity, damp);			
		}
		proximity = Vector2.Distance (target.position, transform.position);
		anim.SetFloat (BossAnim.Proximity, proximity);
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
