using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public ParticleSystem part;
	GameObject m_GroundCheck;
	public float k_GroundedRadius;

	void OnEnable(){
		m_GroundCheck = GameObject.FindWithTag("GroundCheck");
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode(){
		//Create game object where player is facing
		//physics.overlapcircle
		//get all gameobjects that collide
		//deactivate gameobjects that aren't player (or walls)

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.transform.position, k_GroundedRadius);
        foreach(Collider2D col in colliders)
        {
            if (col.gameObject.tag == "Platform"){
            	col.gameObject.SetActive(false);
//            	return;
            }
        }
//        part.Play();
//		Debug.Log("Explode");
//		part.SetActive(false);
	}
}