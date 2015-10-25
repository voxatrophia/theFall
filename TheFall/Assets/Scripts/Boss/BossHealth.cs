using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	public Color damageColor;
	public AudioClip damageSound;
	public AudioClip nearDeathSound;
	public AudioClip victoryTheme;

	[Range(0, 10)]
	public int bossHealth = 10;

	AudioSource audioSrc;
	FlashSpriteColor flash;

	void Start () {
		flash = GetComponent<FlashSpriteColor>();
		audioSrc = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(Tags.PlayerAttack)){
			bossHealth -= 1;

			if(bossHealth > 1){
				audioSrc.PlayOneShot(damageSound);
				flash.FlashSprite(damageColor);
			}
			else if(bossHealth == 1){
				AudioManager.Instance.SwitchMusic(nearDeathSound);
				flash.ChangeSpriteColor(damageColor);
				EventManager.TriggerEvent(Events.BossNearDeath);
			}
			else if(bossHealth == 0){
				StartCoroutine(BossDeath());
			}
		}
	}


	IEnumerator BossDeath(){
		AudioManager.Instance.SwitchMusic(victoryTheme);
		EventManager.TriggerEvent(Events.Victory);
		Time.timeScale = 0;
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1f));
		EventManager.TriggerEvent(Events.DeathFade);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1.5f));
        Time.timeScale = 1;
        MainController.SwitchScene(Scenes.Victory);
	}

}
