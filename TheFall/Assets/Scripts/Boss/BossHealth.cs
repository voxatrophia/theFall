using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	public Color damageColor;

	FlashSpriteColor flash;

	void Start () {
		flash = GetComponent<FlashSpriteColor>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "PlayerAttack"){
			flash.FlashSprite(damageColor);
			Debug.Log("Hit");
		}
	}
}
