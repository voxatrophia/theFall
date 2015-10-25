using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public float groundedRadius = 0.2f;

	GameObject groundCheck;
	GameObject explosionObject;
	Explosion explosion;

	void Awake(){
		//particle system
		explosionObject = GameObject.FindWithTag(Tags.Explosion);
		explosion = explosionObject.GetComponent<Explosion>();
	}

	void OnEnable(){
		groundCheck = GameObject.FindWithTag(Tags.GroundCheck);
	}

	//Called from UseItem script attached to player
	public void Explode(){
		//Using same code as PlatformController2D, even using player ground_check
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundedRadius);
        foreach(Collider2D col in colliders)
        {
            if (col.CompareTag(Tags.Platform)){
            	col.gameObject.SetActive(false);
            }
        }

     	//Explosion particle effect
        explosion.Explode(groundCheck.transform.position);

	}
}