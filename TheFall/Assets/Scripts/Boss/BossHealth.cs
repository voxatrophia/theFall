using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	public Color damageColor;
	public AudioClip damageSound;
	public AudioClip nearDeathSound;
	public AudioClip deathSound;
	public GameObject victory;
	AudioSource victoryAudio;

	[Range(0, 10)]
	public int bossHealth = 10;

	AudioSource audioSrc;
	FlashSpriteColor flash;

	void Start () {
		victoryAudio = victory.GetComponent<AudioSource>();
		flash = GetComponent<FlashSpriteColor>();
		audioSrc = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "PlayerAttack"){
			bossHealth -= 1;

			if(bossHealth > 1){
				audioSrc.PlayOneShot(damageSound);
				flash.FlashSprite(damageColor);
			}
			else if(bossHealth == 1){
				audioSrc.PlayOneShot(nearDeathSound);
				flash.ChangeSpriteColor(damageColor);
				EventManager.TriggerEvent("BossNearDeath");
			}
			else if(bossHealth == 0){
				StartCoroutine(BossDeath());
			}
		}
	}


	IEnumerator BossDeath(){
		victoryAudio.Play();
		EventManager.TriggerEvent("Death");
		Time.timeScale = 0;
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1f));
		EventManager.TriggerEvent("DeathFade");
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1.5f));
        Time.timeScale = 1;
        MainController.SwitchScene("Victory");
	}

}
