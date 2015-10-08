using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	public float k_GroundedRadius;

	GameObject m_GroundCheck;
	GameObject explosionObject;
	Explosion explosion;

	void Awake(){
		//particle system
		explosionObject = GameObject.FindWithTag("Explosion");
		explosion = explosionObject.GetComponent<Explosion>();
	}

	void OnEnable(){
		m_GroundCheck = GameObject.FindWithTag("GroundCheck");
	}

	//Called from UseItem script attached to player
	public void Explode(){
		//Using same code as PlatformController2D, even using player ground_check
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.transform.position, k_GroundedRadius);
        foreach(Collider2D col in colliders)
        {
            if (col.gameObject.tag == "Platform"){
            	col.gameObject.SetActive(false);
            }
        }

     	//Explosion particle effect
        explosion.Explode(m_GroundCheck.transform.position);

	}
}