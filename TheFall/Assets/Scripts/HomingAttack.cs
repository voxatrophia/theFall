using UnityEngine;
using System.Collections;

public class HomingAttack : MonoBehaviour {

	public float damp = 1f;
	public float lifetime = 5f;
	private GameObject target;
	float speed = 10f;
	float curSpeed;
	//private Vector3 velocity = Vector3.zero;

	//OnEnable, start moving toward player
	//start coroutine that destroys object after 3 seconds

	void OnEnable(){
		StartCoroutine("SelfDestruct");
		target = GameObject.FindWithTag("Player");
	}
	
	void Update () {

		Vector3 dest = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
		//Good for now, slows down when close, makes it a bit more managable
		if(Vector3.Distance(dest, transform.position) < 3){
			curSpeed = 5f;
		}
		else{
			curSpeed = speed;
		}

		//This line works, but doesn't get to the player completely, but is easier to dodge...
		// transform.position = Vector3.SmoothDamp (transform.position, dest, ref velocity, damp, 50);
	    transform.position = Vector3.MoveTowards (transform.position, dest, Time.deltaTime * curSpeed);
	}

	IEnumerator SelfDestruct(){
		yield return new WaitForSeconds(lifetime);
		Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			Destroy(this.gameObject);
		}
	}

}
